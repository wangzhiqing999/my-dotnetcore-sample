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



        /// <summary>
        /// 获取空白结果.
        /// </summary>
        public static CommonQueryResult<T> GetEmptyResult()
        {            
            CommonQueryResult<T> result = new CommonQueryResult<T>()
            {
                // 翻页信息
                QueryPageInfo = new PageInfo(10, 1, 0),
                // 数据.
                QueryResultData = new List<T>(),
            };
            return result;            
        }

    }
}
