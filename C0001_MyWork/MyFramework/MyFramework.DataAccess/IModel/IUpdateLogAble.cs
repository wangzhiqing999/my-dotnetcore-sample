using System;
using System.Collections.Generic;
using System.Text;

namespace MyFramework.IModel
{

    /// <summary>
    /// 可记录更新日志的接口.
    /// </summary>
    public interface IUpdateLogAble
    {
        /// <summary>
        /// 创建人
        /// </summary>
        string CreateUser { set; get; }


        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { set; get; }


        /// <summary>
        /// 最后更新人
        /// </summary>
        string LastUpdateUser { set; get; }


        /// <summary>
        /// 最后更新时间
        /// </summary>
        DateTime LastUpdateTime { set; get; }




        /// <summary>
        /// 插入之前的操作.
        /// </summary>
        /// <param name="model"></param>
        void BeforeInsertOperation(string userName);


        /// <summary>
        /// 更新之前的操作.
        /// </summary>
        /// <param name="model"></param>
        void BeforeUpdateOperation(string userName);


    }


}
