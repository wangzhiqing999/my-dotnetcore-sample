using System;
using System.Collections.Generic;
using System.Text;

using W1100_Page.ServiceModel;

namespace W1100_Page.Service
{
    /// <summary>
    /// 财经数据接口.
    /// </summary>
    public interface IFinanceDataService
    {
        /// <summary>
        /// 获取财经数据列表.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FinanceDataListOutput GetFinanceDataList(GetFinanceDataInput input);
    }
}
