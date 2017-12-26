using System;
using System.Collections.Generic;
using System.Text;

using W1100_Page.Model;

namespace W1100_Page.ServiceModel
{

    /// <summary>
    /// 获取财经数据列表的输出.
    /// </summary>
    public class FinanceDataListOutput
    {
        /// <summary>
        /// 财经数据列表.
        /// </summary>
        public List<FinanceData> FinanceDataList { set; get; }




        /// <summary>
        /// 页索引.
        /// </summary>
        public int PageIndex
        {
            set;
            get;
        }



        /// <summary>
        /// 页大小.
        /// </summary>
        public int PageSize
        {
            set;
            get;
        }



        /// <summary>
        /// 行数.
        /// </summary>
        public int RowCount
        {
            set;
            get;
        }



        /// <summary>
        /// 总页数. (只读.)
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                {
                    // 如果页面大小没有设置， 那么直接返回0.
                    return 0;
                }

                if (RowCount == 0)
                {
                    // 如果没有数据，那么直接返回0.
                }


                // 页数 =  行数 / 每页行数.
                int result = RowCount / PageSize;

                if (RowCount % PageSize == 0)
                {
                    // 刚好整除.
                    return result;
                }

                // 剩下的不足的， 最后放一页.
                return result + 1;
            }
        }

    }
}
