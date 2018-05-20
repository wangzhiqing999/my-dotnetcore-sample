using System;
using System.Collections.Generic;
using System.Text;

namespace MyAuthentication.IModel
{

    /// <summary>
    /// 可由用户管理的数据.
    /// </summary>
    public interface IUserManagerAble
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        long UserID { set; get; }
    }

}
