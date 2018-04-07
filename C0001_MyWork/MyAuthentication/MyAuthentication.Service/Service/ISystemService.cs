using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;
using MyFramework.Util;


namespace MyAuthentication.Service
{

    /// <summary>
    /// 系统服务.
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 查询.
        /// </summary>
        /// <returns></returns>
        CommonQueryResult<MySystem> Query(int pageNo = 1, int pageSize = 10);

    }
}
