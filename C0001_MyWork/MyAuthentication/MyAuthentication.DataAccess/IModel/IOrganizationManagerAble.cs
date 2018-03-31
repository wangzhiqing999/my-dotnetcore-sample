using System;
using System.Collections.Generic;
using System.Text;

namespace MyAuthentication.IModel
{

    /// <summary>
    /// 可由组织管理的数据.
    /// </summary>
    public interface IOrganizationManagerAble
    {
        /// <summary>
        /// 组织ID.
        /// </summary>
        long OrganizationID { set; get; }
    }
}
