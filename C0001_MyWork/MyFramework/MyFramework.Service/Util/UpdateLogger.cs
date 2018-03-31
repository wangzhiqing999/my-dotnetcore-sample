using System;

using MyFramework.IModel;

namespace MyFramework.Util
{

    /// <summary>
    /// 更新日志处理器.
    /// </summary>
    public class UpdateLogger
    {
        /// <summary>
        /// 用户名.
        /// </summary>
        private string _userName;


        public UpdateLogger(string userName)
        {
            this._userName = userName;
        }


        /// <summary>
        /// 插入之前的操作.
        /// </summary>
        /// <param name="model"></param>
        public void BeforeInsertOperation(IUpdateLogAble model)
        {
            // 插入时间.
            model.CreateTime = DateTime.Now;
            // 最后更新时间.
            model.LastUpdateTime = DateTime.Now;

            // 创建人.
            model.CreateUser = this._userName;
            // 最后更新人.
            model.LastUpdateUser = this._userName;
        }


        /// <summary>
        /// 更新之前的操作.
        /// </summary>
        /// <param name="model"></param>
        public void BeforeUpdateOperation(IUpdateLogAble model)
        {
            // 最后更新时间.
            model.LastUpdateTime = DateTime.Now;
            // 最后更新人.
            model.LastUpdateUser = this._userName; 
        }

    }
}
