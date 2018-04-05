using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.ServiceModel
{

    /// <summary>
    /// 删除请求.
    /// </summary>
    public class RemoveRequest
    {
        /// <summary>
        /// 用于删除的主键参数.
        /// </summary>
        public dynamic id { set; get; }

    }
}
