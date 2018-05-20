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

    }
}
