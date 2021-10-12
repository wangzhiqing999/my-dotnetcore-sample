using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace B0250_Quartz.Jobs
{
    public class HelloJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("HelloJob is executing.");
        }


    }
}
