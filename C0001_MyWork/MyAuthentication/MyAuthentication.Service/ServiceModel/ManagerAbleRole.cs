using System;
using System.Collections.Generic;
using System.Text;


using MyAuthentication.Model;

namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 可管理角色.
    /// </summary>
    [Serializable]
    public class ManagerAbleRole
    {


        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public ManagerAbleRole()
        {
        }

        public ManagerAbleRole(string roleCode, string roleName)
        {
            this.RoleCode = roleCode;
            this.RoleName = roleName;
        }

        public ManagerAbleRole(MyRole role)
        {
            this.RoleCode = role.RoleCode;
            this.RoleName = role.RoleName;
        }



        /// <summary>
        /// 角色代码
        /// </summary>
        public string RoleCode { set; get; }



        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }



        /// <summary>
        /// 可访问标志.
        /// </summary>
        public bool AccessAble { set; get; }

    }

}
