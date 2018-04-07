using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;

namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 可管理模块.
    /// </summary>
    [Serializable]
    public class ManagerAbleModule
    {

        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public ManagerAbleModule()
        {
        }

        public ManagerAbleModule(string moduleCode, string moduleName)
        {
            this.ModuleCode = moduleCode;
            this.ModuleName = moduleName;
        }

        public ManagerAbleModule(MyModule module)
        {
            this.ModuleCode = module.ModuleCode;
            this.ModuleName = module.ModuleName;            
            if(module.Actions != null)
            {
                this.Actions = new List<ManagerAbleAction>();
                foreach(MyAction action in module.Actions)
                {
                    this.Actions.Add(new ManagerAbleAction(action));
                }
            }
        }


        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleCode { set; get; }



        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { set; get; }



        /// <summary>
        /// 可访问标志.
        /// </summary>
        public bool AccessAble { set; get; }



        /// <summary>
        /// 可管理模块动作.
        /// </summary>
        public List<ManagerAbleAction> Actions { set; get; }

    }
}
