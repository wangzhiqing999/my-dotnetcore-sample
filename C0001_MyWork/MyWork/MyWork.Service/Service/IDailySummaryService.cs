using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;

namespace MyWork.Service
{


    /// <summary>
    /// 日结服务
    /// </summary>
    public interface IDailySummaryService
    {

        /// <summary>
        /// 创建每日总结.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        CommonServiceResult BuildDailySummary(DateTime date);





        /// <summary>
        /// 生成账户的日结报表
        /// </summary>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        CommonServiceResult BuildDailyReport(DateTime reportDate);

    }
}
