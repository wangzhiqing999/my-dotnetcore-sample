using System;
using System.Collections.Generic;
using System.Text;

namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 权限信息.
    /// </summary>
    [Serializable]
    public class PermissionInfo
    {
        /// <summary>
        /// 权限代码.
        /// </summary>
        public string PermissionCode { set; get; }


        /// <summary>
        /// 权限名称.
        /// </summary>
        public string PermissionName { set; get; }


        /// <summary>
        /// 权限路径.
        /// </summary>
        public string PermissionPath { set; get; }
    }

}
