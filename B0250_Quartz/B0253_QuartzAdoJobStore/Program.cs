using System;
using System.Threading.Tasks;
using B0253_QuartzAdoJobStore.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace B0253_QuartzAdoJobStore
{
    class Program
    {




        private static async Task Main(string[] args)
        {


            Program p = new Program();
            p.Init();
            Console.WriteLine("----- Init Finish. -----");



            string cmd;
            do
            {

                Console.WriteLine("Please Input Cmd: 3/4/0");
                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "3":
                        p.TestCron();
                        break;

                    case "4":
                        p.TestDeleteCron();
                        break;


                    case "0":
                        p.ShowInfo();
                        break;
                }



            } while (cmd != "");



            Console.WriteLine("----- Exit. -----");

            p.Finish();

        }




        private StdSchedulerFactory factory;
        private IScheduler scheduler;


        async void Init()
        {
            factory = new StdSchedulerFactory();
            scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();
        }


        async void Finish()
        {
            await scheduler.Shutdown();
        }



        /// <summary>
        /// 测试使用  Cron 表达式.
        /// 
        /// 测试方式是：
        /// 先调用 TestCron() 创建， 观察运行。
        /// 后调用 ShowInfo()，观察作业的配置（一行数据）。
        /// 再调用 TestDeleteCron() 删除。
        /// 最后调用 ShowInfo()，观察作业的配置（空白）。
        /// </summary>
        void TestCron()
        {
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("HelloJob", "DemoGroup")
                .WithDescription("简单的 Hello World 的作业.")
                .Build();


            // 使用 Cron 表达式， 时间在 0秒 的时候触发.
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myCronTrigger", "DemoGroup")
                .WithDescription("使用 Cron 表达式， 时间在 0秒 的时候触发。")
                .WithCronSchedule("0 * * * * ? *")
                .Build();


            scheduler.ScheduleJob(job, trigger);
        }



        /// <summary>
        /// 测试删除 使用  Cron 表达式 的作业.
        /// </summary>
        void TestDeleteCron()
        {
            JobKey jobKey = JobKey.Create("HelloJob", "DemoGroup");
            scheduler.DeleteJob(jobKey);
        }



        void ShowInfo()
        {
            var allJobGroupNames = scheduler.GetJobGroupNames().Result;
            foreach (var groupName in allJobGroupNames)
            {
                Console.WriteLine($"========== Job Group : {groupName} ==========");

                GroupMatcher<JobKey> groupMatcher = GroupMatcher<JobKey>.GroupEquals(groupName);
                var allJobKeys = scheduler.GetJobKeys(groupMatcher).Result;
                foreach (var jobKey in allJobKeys)
                {
                    Console.WriteLine($"===== Job Key : {jobKey.Group} - {jobKey.Name} =====");


                    var jobDetail = scheduler.GetJobDetail(jobKey).Result;

                    Console.WriteLine($"===== Job Detail : {jobDetail.JobType.FullName} | {jobDetail.Description}  =====");
                }
            }






            var allTriggerGroupNames = scheduler.GetTriggerGroupNames().Result;
            foreach (var groupName in allTriggerGroupNames)
            {
                Console.WriteLine($"========== Trigger Group : {groupName} ==========");

                GroupMatcher<TriggerKey> groupMatcher = GroupMatcher<TriggerKey>.GroupEquals(groupName);
                var allTriggerKey = scheduler.GetTriggerKeys(groupMatcher).Result;

                foreach (var triggerKey in allTriggerKey)
                {
                    Console.WriteLine($"===== Trigger Key : {triggerKey.Group} - {triggerKey.Name} =====");

                    var trigger = scheduler.GetTrigger(triggerKey).Result;

                    Console.WriteLine($"===== Trigger :  {trigger.Description}  | {trigger.JobKey.Group} - {trigger.JobKey.Name} =====");

                }
            }
        }


    }
}
