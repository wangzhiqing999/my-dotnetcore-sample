using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using P0002_MyGrid.Service;
using P0002_MyGrid.DataAccess;
using P0002_MyGrid.Model;
using P0002_MyGrid.ServiceModel;


namespace P0002_MyGrid.ServiceImpl
{
    public class DefaultGridService : IGridService
    {



        private readonly MyGridContext _MyGridContext;


        private readonly ILogger<DefaultGridService> _Logger;



        public DefaultGridService(MyGridContext context, ILogger<DefaultGridService> logger)
        {
            this._MyGridContext = context;
            this._Logger = logger;
        }



        /// <summary>
        /// 初始化网格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Grid> GetInitGrids(InitGridRequest request)
        {
            List<Grid> resultList = new List<Grid>();

            // 每个单位的价格 = （最高价 - 最低价）/ 网格数.
            decimal onePrice = (request.MaxPrice - request.MinPrice) / request.NumOfGrid;

            for (int i = 1; i <= request.NumOfGrid; i++)
            {
                Grid grid = new Grid();
                // 商品代码.
                grid.ItemCode = request.ItemCode;
                // 网格代码.
                grid.GridNo = i;

                // 买入价.
                grid.BuyPrice = request.MinPrice + (onePrice * (i - 1));
                // 卖出价.
                grid.SellPrice = request.MinPrice + (onePrice * i);

                resultList.Add(grid);
            }


            return resultList;
        }



        /// <summary>
        /// 存储网格.
        /// </summary>
        /// <param name="grids"></param>
        /// <returns></returns>
        public CommonServiceResult SaveGrids(List<Grid> grids)
        {
            try
            {
                foreach (var grid in grids)
                {
                    _MyGridContext.Grids.Add(grid);
                }
                _MyGridContext.SaveChanges();


                return CommonServiceResult.DefaultSuccessResult;
            } 
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new CommonServiceResult (ex);
            }


         
        }



        /// <summary>
        /// 新网格交易.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CommonServiceResult NewTransaction(NewTransactionRequest request)
        {
            try
            {
                Grid grid = _MyGridContext.Grids.FirstOrDefault(p => p.ItemCode == request.ItemCode && p.GridNo == request.GridNo);

                if(grid == null)
                {
                    _Logger.LogError($"Grid ({request.ItemCode}, {request.GridNo}) Not Found!");
                    return CommonServiceResult.CreateFailResult("GRID_NOT_FOUND", "网格数据不存在。");
                }

                if(request.TransactionQuantity== 0)
                {
                    return CommonServiceResult.CreateFailResult("TRANSACTION_QUANTITY_IS_ZERO", "成交数量为零。");
                }


                GridTransaction gridTransaction = new GridTransaction()
                {
                    ItemCode = request.ItemCode,
                    GridNo = request.GridNo,
                    TransactionDate = request.TransactionDate,
                    TransactionPrice= request.TransactionPrice,
                    TransactionQuantity = request.TransactionQuantity,
                };

                this._MyGridContext.GridTransactions.Add(gridTransaction);


                // 修改网格的 当前持仓.

                // 正数是买入操作，负数是卖出操作，这里简单做 += 操作即可.
                grid.Hold += request.TransactionQuantity;


                this._MyGridContext.SaveChanges();

                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new CommonServiceResult(ex);
            }
        }






        /// <summary>
        /// 获取配置了网格的商品代码列表.
        /// </summary>
        /// <returns></returns>
        public List<string> GetItemCodes()
        {
            List<string> resultList = new List<string>();


            var query =
                from data in _MyGridContext.Grids.ToList()
                group data by data.ItemCode;

            foreach(var g in query)
            {
                resultList.Add(g.Key);
            }
            
            return resultList;
        }



        /// <summary>
        /// 获取商品的网格列表
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<Grid> GetItemGrids(string itemCode)
        {
            var query =
                from data in this._MyGridContext.Grids
                where 
                    data.ItemCode == itemCode
                orderby 
                    data.GridNo
                select 
                    data;

            return query.ToList();
        }




        /// <summary>
        /// 获取针对商品，需要做的操作.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<GetTodoResponse> GetTodoList(GetTodoRequest request)
        {

            List <GetTodoResponse> resultList = new List< GetTodoResponse >();

            try
            {

                // 先加载所有网格数据.
                var query =
                    from data in this._MyGridContext.Grids
                    where
                        data.ItemCode == request.ItemCode
                    orderby
                        data.GridNo
                    select
                        data;

                List<Grid> grids = query.ToList();


                // 获取当前价位所在的网格.
                Grid currentGrid = grids.FirstOrDefault(p=> p.BuyPrice < request.CurrentPrice && p.SellPrice > request.CurrentPrice);

                if (currentGrid == null)
                {
                    // 已经超出网格范围了.

                    GetTodoResponse response = new GetTodoResponse()
                    {
                        ItemCode= request.ItemCode,
                        Todo= DoWhat.None,
                    };
                    resultList.Add(response);

                    return resultList;
                }


                // 默认买入数量.
                int defaultBuyQuantity = 1000;
                Grid lastGrid = grids.Last();
                if(lastGrid.Hold > 0)
                {
                    defaultBuyQuantity = lastGrid.Hold;
                }

                // 判断是否有持仓.
                if (currentGrid.Hold > 0)
                {
                    // 有持仓.
                    // 当前网格，挂 卖出的单子.

                    GetTodoResponse sellResponse = new GetTodoResponse()
                    {
                        ItemCode = request.ItemCode,
                        Todo = DoWhat.Sell,
                        GridNo = currentGrid.GridNo,
                        Price = currentGrid.SellPrice,
                        Quantity = currentGrid.Hold,
                    };
                    resultList.Add(sellResponse);



                    // 尝试去上一个网格， 挂一个 买入的单子.
                    if(currentGrid.GridNo > 1)
                    {
                        var prevGrid = grids.FirstOrDefault(p => p.GridNo == currentGrid.GridNo - 1);

                        GetTodoResponse buyResponse = new GetTodoResponse()
                        {
                            ItemCode = request.ItemCode,
                            Todo = DoWhat.Buy,
                            GridNo = prevGrid.GridNo,
                            Price = prevGrid.BuyPrice,
                            Quantity = defaultBuyQuantity,
                        };
                        resultList.Add(buyResponse);
                    }
                    



                } 
                else
                {
                    // 没持仓.

                    // 当前网格，挂 买入的单子.
                    GetTodoResponse buyResponse = new GetTodoResponse()
                    {
                        ItemCode = request.ItemCode,
                        Todo = DoWhat.Buy,
                        GridNo = currentGrid.GridNo,
                        Price = currentGrid.BuyPrice,
                        Quantity = defaultBuyQuantity,
                    };
                    resultList.Add(buyResponse);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new List<GetTodoResponse>();
            }

        }
    }

}
