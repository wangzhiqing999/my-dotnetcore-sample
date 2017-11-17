using System;
using MyMiniTradingSystem.Model;

namespace MyMiniTradingSystem.Service
{



    /// <summary>
    /// 每日汇总服务.
    /// </summary>
    public interface IDailySummaryService
    {


        /// <summary>
        /// 创建某用户的每日总结
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ServiceResult BuildOneUserDailySummary(string userCode, DateTime date);



        /// <summary>
        /// 更新每日总结
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        ServiceResult InsertOrUpdateDailySummary(DailySummary newData);
    }
}