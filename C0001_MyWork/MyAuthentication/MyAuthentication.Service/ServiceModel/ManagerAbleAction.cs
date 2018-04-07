using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;


namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 可管理动作.
    /// </summary>
    [Serializable]
    public class ManagerAbleAction
    {
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public ManagerAbleAction()
        {
        }

        public ManagerAbleAction(string actionCode, string actionName)
        {
            this.ActionCode = actionCode;
            this.ActionName = actionName;
        }

        public ManagerAbleAction(MyAction action)
        {
            this.ActionCode = action.ActionCode;
            this.ActionName = action.ActionName;
        }

        /// <summary>
        /// 动作代码
        /// </summary>
        public string ActionCode { set; get; }


        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { set; get; }


        /// <summary>
        /// 可访问标志.
        /// </summary>
        public bool AccessAble { set; get; }
    }
}
