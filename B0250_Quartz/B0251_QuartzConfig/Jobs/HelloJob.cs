﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;

namespace B0251_QuartzConfig.Jobs
{
    public class HelloJob : IJob
    {



        public Task Execute(IJobExecutionContext context)
        {
            Console.Out.WriteLine("HelloJob Start!");

            return Task.CompletedTask;
        }


    }
}
