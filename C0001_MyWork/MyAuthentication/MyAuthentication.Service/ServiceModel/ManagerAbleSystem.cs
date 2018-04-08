using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;


namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 可管理系统
    /// </summary>
    [Serializable]
    public class ManagerAbleSystem
    {


        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public ManagerAbleSystem()
        {
        }

        public ManagerAbleSystem(string systemCode, string systemName)
        {
            this.SystemCode = systemCode;
            this.SystemName = systemName;
        }

        public ManagerAbleSystem(MySystem system)
        {
            this.SystemCode = system.SystemCode;
            this.SystemName = system.SystemName;

            if (system.Roles != null)
            {
                this.Roles = new List<ManagerAbleRole>();
                foreach (MyRole role in system.Roles)
                {
                    this.Roles.Add(new ManagerAbleRole(role));
                }
            }
        }




        /// <summary>
        /// 系统代码
        /// </summary>
        public string SystemCode { set; get; }


        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { set; get; }



        /// <summary>
        /// 可访问标志.
        /// </summary>
        public bool AccessAble { set; get; }



        /// <summary>
        /// 可管理系统角色.
        /// </summary>
        public List<ManagerAbleRole> Roles { set; get; }

    }
}
