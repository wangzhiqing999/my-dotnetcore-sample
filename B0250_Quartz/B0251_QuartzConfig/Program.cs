using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace B0251_QuartzConfig
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(10));




            Console.WriteLine("----- Press Enter To Quit. -----");
            Console.ReadLine();


            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();
        }
    }
}
