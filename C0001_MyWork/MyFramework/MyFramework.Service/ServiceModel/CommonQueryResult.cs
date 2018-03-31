using System;
using System.Collections.Generic;
using System.Text;
using MyFramework.Util;

namespace MyFramework.ServiceModel
{

    /// <summary>
    /// 通用的查询结果.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CommonQueryResult<T>
    {

        /// <summary>
        /// 翻页信息.
        /// </summary>
        public PageInfo QueryPageInfo { set; get; }

        /// <summary>
        /// 结果数据.
        /// </summary>
        public List<T> QueryResultData { set; get; }

    }
}
