using System;
using System.Collections.Generic;
using System.Text;

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1001_ABP_With_Zero.Tasks.Dtos;



namespace W1001_ABP_With_Zero.Tasks
{

    /// <summary>
    /// 测试用户类型服务.
    /// </summary>
    public interface ITestUserTypeAppService
    {


        /// <summary>
        /// 创建用户类型.
        /// 
        /// （本方法为 测试 事务处理的。）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Create(CreateTestUserTypeInput input);


    }
}
