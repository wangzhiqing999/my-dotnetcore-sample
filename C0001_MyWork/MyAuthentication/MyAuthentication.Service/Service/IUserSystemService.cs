using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;
using MyAuthentication.ServiceModel;


namespace MyAuthentication.Service
{

    /// <summary>
    /// 用户-系统关系服务.
    /// </summary>
    public interface IUserSystemService
    {

        /// <summary>
        /// 获取指定用户的 可管理模块列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<ManagerAbleSystem> GetManagerAbleSystemByUserID(long userID);


        /// <summary>
        /// 更新指定用户的 可管理模块列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        CommonServiceResult UpdateManagerAbleSystem(long userID, List<ManagerAbleSystem> dataList);

    }
}
