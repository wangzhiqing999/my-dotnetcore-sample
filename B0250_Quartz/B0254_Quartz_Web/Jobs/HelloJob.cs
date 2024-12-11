using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace B0254_Quartz_Web.Jobs
{
    public class HelloJob : IJob
    {


        
        private readonly ILogger<HelloJob> _logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            _logger = logger;
        }
      


        public Task Execute(IJobExecutionContext context)
        {
            // Console.Out.WriteLine("HelloJob Start!");

            _logger.LogInformation("--- HelloJob Start! ---");


            return Task.CompletedTask;
        }


    }
}
