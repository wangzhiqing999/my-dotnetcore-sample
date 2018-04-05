using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;


namespace MyFramework.Util
{

    /// <summary>
    /// 翻页处理.
    /// </summary>
    [Serializable]
    public class PageInfo
    {

        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public PageInfo()
        {

        }


        /// <summary>
        /// 获取所有数据的 PageInfo
        /// </summary>
        /// <returns></returns>
        public static PageInfo GetAllDataPageInfo()
        {
            PageInfo result = new PageInfo();
            result.PageSize = Int16.MaxValue;
            return result;
        }



         /// <summary>
        /// 可选数据构造函数
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        public PageInfo(int? pageSize, int? pageNo)
        {
            this.PageSize = 10;
            // 设置翻页.
            if (pageSize != null && pageSize > 0)
            {
                this.PageSize = pageSize.Value;
            }

            if (pageNo != null && pageNo > 0)
            {
                this.PageIndex = pageNo.Value;
            }
        }





        /// <summary>
        /// 可选数据构造函数
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        public PageInfo(int? pageSize, int? pageNo, int rowCount)
        {
            this.PageSize = 10;
            // 设置翻页.
            if (pageSize != null && pageSize > 0)
            {
                this.PageSize = pageSize.Value;
            }

            // 设置总行数
            this.RowCount = rowCount;

            // 如果第几页数据有效， 设置第几页.
            if (pageNo != null && pageNo.Value > 0 && pageNo.Value <= this.PageCount)
            {
                this.PageIndex = pageNo.Value;
            }
        }




        /// <summary>
        /// 默认第几页.
        /// </summary>
        private int m_PageIndex = 1;

        /// <summary>
        /// 页索引.
        /// </summary>
        public int PageIndex
        {
            set
            {
                if (value <= 0)
                {
                    // 忽略非法的 页面大小数据.
                    return;
                }
                m_PageIndex = value;
            }
            get
            {
                return m_PageIndex;
            }
        }




        /// <summary>
        /// 默认的页面大小 = 10.
        /// </summary>
        private int m_PageSize = 10;

        /// <summary>
        /// 页大小.
        /// </summary>
        public int PageSize
        {
            set
            {
                if (value <= 0)
                {
                    // 忽略非法的 页面大小数据.
                    return;
                }
                m_PageSize = value;
            }
            get
            {
                return m_PageSize;
            }
        }



        private int m_RowCount = 0;

        /// <summary>
        /// 行数.
        /// </summary>
        public int RowCount
        {
            set
            {
                // 赋值.
                m_RowCount = value;

                // 第几页数据修正.
                if (PageCount > 0 && PageCount < m_PageIndex)
                {
                    // 页数 大于 最后一页的话.
                    // 修正为最后一页.
                    m_PageIndex = PageCount;
                }
            }
            get
            {
                return m_RowCount;
            }
        }




        /// <summary>
        /// 总页数. (只读.)
        /// </summary>
        public int PageCount
        {
            get
            {
                if (m_PageSize == 0)
                {
                    // 如果页面大小没有设置， 那么直接返回0.
                    return 0;
                }

                if (RowCount == 0)
                {
                    // 如果没有数据，那么直接返回0.
                }


                // 页数 =  行数 / 每页行数.
                int result = RowCount / m_PageSize;

                if (RowCount % m_PageSize == 0)
                {
                    // 刚好整除.
                    return result;
                }

                // 剩下的不足的， 最后放一页.
                return result + 1;
            }
        }



        /// <summary>
        /// 查询时，需要跳过的行数.
        /// </summary>
        [JsonIgnore]
        public int SkipValue
        {
            get
            {
                return (m_PageIndex - 1) * m_PageSize;
            }
        }





        /// <summary>
        /// 拥有上一页.
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return this.PageIndex > 1;
            }
        }


        /// <summary>
        /// 拥有下一页.
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return this.PageIndex < this.PageCount;
            }
        }

    }


}
