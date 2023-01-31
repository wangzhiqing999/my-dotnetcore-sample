using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using P0002_MyGrid.Model;
using P0002_MyGrid.ServiceModel;

namespace P0002_MyGrid.Service
{

    /// <summary>
    /// 网格服务.
    /// </summary>
    public interface IGridService
    {

        /// <summary>
        /// 初始化网格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<Grid> GetInitGrids(InitGridRequest request);



        /// <summary>
        /// 存储网格.
        /// </summary>
        /// <param name="grids"></param>
        /// <returns></returns>
        CommonServiceResult SaveGrids(List<Grid> grids);



        /// <summary>
        /// 新交易.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CommonServiceResult NewTransaction(NewTransactionRequest request);






        /// <summary>
        /// 获取配置了网格的商品代码列表.
        /// </summary>
        /// <returns></returns>
        List<string> GetItemCodes();


        /// <summary>
        /// 获取商品的网格列表.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        List<Grid> GetItemGrids(string itemCode);






        /// <summary>
        /// 获取针对商品，需要做的操作.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<GetTodoResponse> GetTodoList(GetTodoRequest request);

    }
}
