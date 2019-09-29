using System;
using System.Collections.Generic;
using System.Text;



namespace MySimpleAccessCount.Service
{

    /// <summary>
    /// 简单访问计数服务.
    /// </summary>
    public interface ISimpleAccessCountService
    {

        /// <summary>
        /// 访问页面，并返回计数.
        /// </summary>
        /// <param name="pageCode"></param>
        /// <returns></returns>
        long AccessCount(string pageCode);
    }
}
