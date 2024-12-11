using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using B0250_Quartz.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;


namespace B0250_Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            bool admin = false;
            if (args.Length == 1 && args[0] == "admin")
            {
                admin = true;
            }

            Program p = new Program();
            p.Init(admin);

            Console.WriteLine("----- Init Finish. -----");

            string cmd;
            do
            {

                Console.WriteLine("0: ShowInfo");
                Console.WriteLine("1: TestPersistJob");
                Console.WriteLine("2: TestRecoveryJob");
                Console.WriteLine("3: TestCron");
                Console.WriteLine("4: TestDeleteCron");

                Console.WriteLine("5: TestReportWorkJob");
                Console.WriteLine("6: TestDeleteReportWorkJob");

                Console.WriteLine("7: TestRetryJob");
                //

                Console.WriteLine("Please Input Cmd: 1/2/3/4/5/6/7/0");

                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "1":
                        p.TestPersistJob();
                        break;

                    case "2":
                        p.TestRecoveryJob();
                        break;

                    case "3":
                        p.TestCron();
                        break;

                    case "4":
                        p.TestDeleteCron();
                        break;

                    case "5":
                        p.TestReportWorkJob();
                        break;

                    case "6":
                        p.TestDeleteReportWorkJob();
                        break;

                    case "7":
                        p.TestRetryJob();
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




        /// <summary>
        /// 初始化.
        /// </summary>
        /// <param name="admin">
        /// 为 false 时，是正常的运行。 
        /// 为 true 时，自身没有运行的功能，只有管理的功能，也就是 可以 新增、删除 作业。 但是自己不运行作业。
        /// </param>
        async void Init(bool admin = false)
        {

            NameValueCollection properties = new NameValueCollection();

            properties["quartz.scheduler.instanceName"] = "TestScheduler";
            properties["quartz.scheduler.instanceId"] = "instance_one";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";





            // 下面的配置信息，是将作业的数据， 由默认的存储在内存，变更为存储在数据库。
            // 使得作业发生故障时，能够从数据库，重新加载数据，进行恢复的处理.
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            properties["quartz.jobStore.clustered"] = "true";

            // https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/job-stores.html#ado-net-job-store-adojobstore
            properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";
            properties["quartz.dataSource.default.connectionString"] = "Server=pve002;Database=quartznet;Uid=root;Pwd=123456;Charset=utf8mb4";
            properties["quartz.dataSource.default.provider"] = "MySql";
            properties["quartz.jobStore.useProperties"] = "true";
            properties["quartz.serializer.type"] = "json";






            /*

            // https://www.quartz-scheduler.net/documentation/quartz-3.x/configuration/reference.html#remoting-server-and-client
            // #####  Remoting only works with .NET Full Framework. It's also considered unsafe. #####

            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "555";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";
            properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";

            */
            


            factory = new StdSchedulerFactory(properties);
            scheduler = await factory.GetScheduler();


            if (!admin)
            {
                await scheduler.Start();
            }

        }



        async void Finish()
        {
            await scheduler.Shutdown();
        }




        /// <summary>
        /// 测试 PersistJob.
        /// </summary>
        async void TestPersistJob()
        {
            IJobDetail job = JobBuilder.Create<PersistJob>()
                // WithIdentity：作业的唯一标识.
                .WithIdentity("PersistJob", "DemoGroup")
                // WithDescription：作业的描述信息.
                .WithDescription("持久化JobData的作业.")
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("PersistTrigger", "DemoGroup")
                .WithDescription("立即执行一次！然后再每间隔5秒执行一次，重复10次。")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .WithRepeatCount(10))
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }




        /// <summary>
        /// 测试 RequestRecovery.
        /// </summary>
        void TestRecoveryJob()
        {

            // 计划运行 5 个作业.
            for (int i = 0; i < 5; i++)
            {
                IJobDetail job = JobBuilder.Create<RecoveryJob>()
                // WithIdentity：作业的唯一标识.
                .WithIdentity($"RecoveryJob_{i}", "DemoGroup")
                // WithDescription：作业的描述信息.
                .WithDescription("用于测试 RequestRecovery 的作业.")
                .RequestRecovery(true)
                .Build();


                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity($"RecoveryTrigger_{i}", "DemoGroup")
                    .WithDescription("立即执行！然后再每间隔5秒执行一次，重复10次。")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .WithRepeatCount(10))
                    .Build();

                scheduler.ScheduleJob(job, trigger);
            }
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
        async void TestCron()
        {

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("HelloJob", "DemoGroup")
                .WithDescription("简单的 Hello World 的作业.")
                .Build();


            // 使用 Cron 表达式， 时间在 0秒 的时候触发.
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("myCronTrigger1", "DemoGroup")
                .WithDescription("使用 Cron 表达式， 时间在 0秒 的时候触发。")
                .WithCronSchedule("0 * * * * ? *")
                .Build();

            // 使用 Cron 表达式， 时间在 30秒 的时候触发.
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("myCronTrigger2", "DemoGroup")
                .WithDescription("使用 Cron 表达式， 时间在 30秒 的时候触发。")
                .WithCronSchedule("30 * * * * ? *")
                .Build();


            // 这里是单纯的为了测试多个 Trigger 的情况.
            // 0秒 或 30秒触发， 可以简单一个 "0,30 * * * * ? *" 来实现。
            ITrigger[] triggers = new ITrigger[] { trigger1 , trigger2 };

            await scheduler.ScheduleJob(job, triggers, true);
        }



        /// <summary>
        /// 测试删除 使用  Cron 表达式 的作业.
        /// </summary>
        void TestDeleteCron()
        {
            JobKey jobKey = JobKey.Create("HelloJob", "DemoGroup");
            scheduler.DeleteJob(jobKey);
        }




        /// <summary>
        /// 测试 ReportWorkJob.
        /// </summary>
        void TestReportWorkJob()
        {
            // 时间的标签.
            string dtFlag = "0"; // DateTime.Now.ToString("yyyyMMddHHmm");

            JobDataMap jobDataMap = new JobDataMap();
            jobDataMap.Add("ReportId", "123");
            jobDataMap.Add("ReportParams", "x=1&y=2&z=3");

            IJobDetail job = JobBuilder.Create<ReportWorkJob>()
                .WithIdentity($"ReportWorkJob_{dtFlag}")
                .WithDescription("报表作业.")
                .UsingJobData(jobDataMap)
                .Build();


            
            // 使用 Cron 表达式， 时间在 15秒 的时候触发.
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"ReportWorkTrigger_{dtFlag}")
                .WithDescription("使用 Cron 表达式， 时间在 15秒 的时候触发。")
                .WithCronSchedule("15 * * * * ? *")
                .Build();

            // 这里是为了测试，所以是在每分钟的 15秒 的时候，触发一次.
            // 实际工作的报表，是需要专门设置 Cron 的。
            // 日报有日报的 Cron。
            // 周报有周报的 Cron。
            // 月报有月报的 Cron。

            scheduler.ScheduleJob(job, trigger);

        }


        void TestDeleteReportWorkJob()
        {
            JobKey jobKey = JobKey.Create("ReportWorkJob_0");
            scheduler.DeleteJob(jobKey);
        }




        /// <summary>
        /// 测试出错重试的作业.
        /// </summary>
        async void TestRetryJob()
        {
            IJobDetail job = JobBuilder.Create<RetryJob>()
                .WithIdentity("RetryJob", "DemoGroup")
                .WithDescription("会返回执行错误的作业.")
                .Build();

            await scheduler.AddJob(
                job,
                replace: true,
                storeNonDurableWhileAwaitingScheduling: true);

            await scheduler.TriggerJob(job.Key);
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



            Console.WriteLine();


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
