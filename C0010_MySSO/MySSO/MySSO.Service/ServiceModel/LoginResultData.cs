using System;
using System.Collections.Generic;
using System.Text;

namespace MySSO.ServiceModel
{

    /// <summary>
    /// 登录结果数据.
    /// </summary>
    public class LoginResultData
    {

        /// <summary>
        /// 用户 ID.
        /// </summary>
        public Guid UserID { set; get; }


        /// <summary>
        /// Token ID.
        /// </summary>
        public Guid TokenID { set; get; }

    }

}
