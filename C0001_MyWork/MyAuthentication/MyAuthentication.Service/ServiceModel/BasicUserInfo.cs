using System;
using System.Collections.Generic;
using System.Text;

namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 基本用户信息.
    /// </summary>
    [Serializable]
    public class BasicUserInfo
    {
        /// <summary>
        /// 用户ID.
        /// </summary>
        public long UserID { set; get; }



        /// <summary>
        /// 组织ID.
        /// </summary>
        public long OrganizationID { set; get; }



        /// <summary>
        /// 用户名.
        /// </summary>
        public string UserName { set; get; }

    }
}
