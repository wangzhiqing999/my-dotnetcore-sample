using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0002_MyTrading.Model;
using P0002_MyTrading.ServiceModel;


namespace P0002_MyTrading.Service
{


    public interface IHoldingService
    {


        /// <summary>
        /// 存储持仓日志.
        /// <br/>
        /// 这个预期是在 访问基金网站，获取最新 基金净值之后， 调用这个方法来存储数据。
        /// </summary>
        /// <param name="holdingLog"></param>
        /// <returns></returns>
        ServiceResult SaveHoldingLog(HoldingLog holdingLog);







        /// <summary>
        /// 获取最新的持仓日志.
        /// <br/>
        /// 这个预期是在周末的时候，查询上周所有持仓金额的时候调用。
        /// 最终的画面，是每一个持仓的当前持仓净值， 以及一个 总金额的合计。
        /// </summary>
        /// <returns></returns>
        List<HoldingReport> GetLastHoldingReport();




        /// <summary>
        /// 获取历史的持仓日志.
        /// <br/>
        /// 这个预期是在查询单个商品的持仓收益趋势的时候使用。
        /// 最终画面，是显示单个商品的 持仓净值的 走势。
        /// 预期是，对于已经有一定盈利的产品，在盈利发生一定比例的回撤时， 要判断，是不是离场。
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        List<HoldingLog> GetHoldingLogs(string itemCode);


    }


}
