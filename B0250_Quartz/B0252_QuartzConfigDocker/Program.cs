using System;
using System.Threading;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;


namespace B0252_QuartzConfigDocker
{
    class Program
    {

        private static readonly AutoResetEvent _closingEvent = new AutoResetEvent(false);


        private static async Task Main(string[] args)
        {
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(10));


            Console.WriteLine("Start！");

            Console.CancelKeyPress += ((s, a) =>
            {
                Console.WriteLine("程序已退出！");
                _closingEvent.Set();
            });
            _closingEvent.WaitOne();


            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();
        }
    }
}
