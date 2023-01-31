using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;



using P0002_MyTrading.Service;
using P0002_MyTrading.DataAccess;
using P0002_MyTrading.Model;
using P0002_MyTrading.ServiceModel;


namespace P0002_MyTrading.ServiceImpl
{
    public class DefaultHoldingService : IHoldingService
    {


        private readonly MyTradingContext _MyTradingContext;


        private readonly ILogger<DefaultHoldingService> _Logger;


        public DefaultHoldingService(MyTradingContext context, ILogger<DefaultHoldingService> logger)
        {
            this._MyTradingContext = context;
            this._Logger = logger;
        }



        public ServiceResult SaveHoldingLog(HoldingLog holdingLog)
        {

            try
            {
                // 如果存在  相同商品代码+日期的， 先删除.
                var checkQuery =
                    from data in this._MyTradingContext.HoldingLogs
                    where
                        data.ItemCode == holdingLog.ItemCode
                        && data.LogDate == holdingLog.LogDate
                    select
                        data;

                List<HoldingLog> removeList = checkQuery.ToList();
                if(removeList.Count > 0)
                {
                    foreach(var item in removeList)
                    {
                        this._MyTradingContext.HoldingLogs.Remove(item);
                    }
                }

                this._MyTradingContext.HoldingLogs.Add(holdingLog);
                this._MyTradingContext.SaveChanges();

                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex, ex.Message);
                return new ServiceResult(ex);
            }
        }





        public List<HoldingReport> GetLastHoldingReport()
        {
            List<HoldingReport> resultList = new List<HoldingReport>();


            List<Holding> holdings = this._MyTradingContext.Holdings.ToList();
            foreach (Holding holding in holdings)
            {
                var query =
                    from data in this._MyTradingContext.HoldingLogs
                    where
                        data.ItemCode== holding.ItemCode
                    orderby 
                        data.LogDate descending
                    select data;

                var lastData = query.FirstOrDefault();

                if (lastData!=null)
                {
                    HoldingReport result = new HoldingReport ();

                    result.ItemCode = lastData.ItemCode;
                    result.ItemName = holding.ItemName;
                    result.ReaderName = holding.ReaderName;

                    result.LogDate = lastData.LogDate;

                    result.Price = lastData.Price;
                    result.Quantity = lastData.Quantity;

                    resultList.Add (result);

                }

            }

            return resultList;
        }




        public List<HoldingLog> GetHoldingLogs(string itemCode)
        {


            var query =
                    from data in this._MyTradingContext.HoldingLogs
                    where
                        data.ItemCode == itemCode
                    orderby
                        data.LogDate
                    select data;


            return query.ToList();

        }
    }
}
