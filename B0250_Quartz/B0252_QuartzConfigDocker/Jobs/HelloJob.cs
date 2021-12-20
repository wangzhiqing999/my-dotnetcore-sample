using System;
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
            // 为了测试 Docker 里面的时区问题, 在这里额外输出一下 DateTime.Now.
            Console.Out.WriteLine($"HelloJob Start at {DateTime.Now:yyyy-MM-dd HH:mm:ss}!");

            return Task.CompletedTask;
        }


    }
}
