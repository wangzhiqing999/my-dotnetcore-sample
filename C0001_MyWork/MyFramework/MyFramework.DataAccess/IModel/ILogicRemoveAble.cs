using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.IModel
{
    /// <summary>
    /// 可进行逻辑删除的接口.
    /// </summary>
    public interface ILogicRemoveAble
    {
        /// <summary>
        /// 是否数据有效.
        /// </summary>
        bool IsActive { set; get; }
    }
}
