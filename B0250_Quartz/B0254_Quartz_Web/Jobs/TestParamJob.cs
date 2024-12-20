﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;



namespace B0254_Quartz_Web.Jobs
{

    /// <summary>
    /// 用于测试 获取 任务参数的例子.
    /// </summary>
    public class TestParamJob : IJob
    {


        public Task Execute(IJobExecutionContext context)
        {
            // 这里的 Key，运行时候的数值 = IJobDetail 的 group.name
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string jobSays = dataMap.GetString("jobSays");
            float myFloatValue = dataMap.GetFloat("myFloatValue");

            Console.Out.WriteLine($"Instance {key} of DumbJob says: {jobSays}, and val is: {myFloatValue}");

            return Task.CompletedTask;
        }




    }



    /// <summary>
    /// 用于测试 获取 任务参数的例子.
    /// (这里参数 使用 依赖注入的方式来处理)
    /// </summary>
    public class TestParam2Job : IJob
    {



        /// <summary>
        /// 外部传入的参数，这里以依赖注入的方式来定义。
        /// </summary>
        public string JobSays { private get; set; }
        public float MyFloatValue { private get; set; }



        public Task Execute(IJobExecutionContext context)
        {
            // 这里的 Key，运行时候的数值 = IJobDetail 的 group.name
            JobKey key = context.JobDetail.Key;

            Console.Out.WriteLine($"Instance {key} of DumbJob says: {JobSays}, and val is: {MyFloatValue}");

            return Task.CompletedTask;
        }

    }
}
