using System;
using System.Collections.Generic;
using System.Text;

namespace W1100_Page.ServiceModel
{

    /// <summary>
    /// 获取财经数据的输入条件.
    /// </summary>
    public class GetFinanceDataInput
    {

        /// <summary>
        /// 第几页.
        /// </summary>
        public int PageNo { set; get; }


        /// <summary>
        /// 页面大小.
        /// </summary>
        public int PageSize { set; get; }
    }
}
