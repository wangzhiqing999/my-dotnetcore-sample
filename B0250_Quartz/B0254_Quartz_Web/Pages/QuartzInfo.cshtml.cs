using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;

using B0254_Quartz_Web.Jobs;
using Quartz.Impl.Matchers;
using System.Collections.Generic;
using static Quartz.Logging.OperationName;
using Microsoft.AspNetCore.SignalR.Protocol;


namespace B0254_Quartz_Web.Pages
{
    public class QuartzInfoModel : PageModel
    {

        private readonly ILogger<QuartzInfoModel> _logger;

        private IScheduler _Scheduler;

        

        public QuartzInfoModel(ISchedulerFactory schedulerFactory, ILogger<QuartzInfoModel> logger)
        {
            _Scheduler = schedulerFactory.GetScheduler().Result;



            _logger = logger;

        }



        public List<string> jobGroupNames { get; set; }

        public Dictionary<string, List<IJobDetail>> groupJobs { get; set; }


        public List<string> triggerGroupNames { get; set; }

        public Dictionary<string, List<ITrigger>> groupTriggers { get; set; }
        


        public void OnGet()
        {

            _logger.LogInformation("QuartzInfo Start!");


            /*

            // ʹ�� Cron ���ʽ�� ʱ���� 0�� ��ʱ�򴥷�.
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myCronTrigger", "DemoTriggerGroup")
                .WithDescription("ʹ�� Cron ���ʽ�� ʱ���� 0�� ��ʱ�򴥷���")
                .WithCronSchedule("0 * * * * ? *")
                .Build();
             
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("HelloJob", "DemoJobGroup")
                .WithDescription("�򵥵� Hello World ����ҵ.")
                .Build();

            var result = _Scheduler.ScheduleJob(job, trigger).Result;

            Console.WriteLine($"ScheduleJob Result = {result}");

           */


            /*
             
            // ʹ�� Cron ���ʽ�� ʱ���� 2�� ��ʱ�򴥷�.
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("myCronTrigger2", "DemoTriggerGroup")
                .WithDescription("ʹ�� Cron ���ʽ�� ʱ���� 2�� ��ʱ�򴥷���")
                .WithCronSchedule("2 * * * * ? *")
                .Build();


            JobDataMap jobDataMap = new JobDataMap();
            jobDataMap.Add("jobSays", "����");
            jobDataMap.Add("myFloatValue", "3.141");


            IJobDetail job2 = JobBuilder.Create<TestParamJob>()
                .WithIdentity("TestParamJob", "DemoJobGroup")
                .WithDescription("�򵥵Ĳ��Բ�������ҵ.")
                // .UsingJobData("jobSays", "����")
                // .UsingJobData("myFloatValue", 3.141f)
                .UsingJobData(jobDataMap)
                .Build();

            var result2 = _Scheduler.ScheduleJob(job2, trigger2).Result;

            Console.WriteLine($"ScheduleJob Result = {result2}");

            */




            groupJobs = new Dictionary<string, List<IJobDetail>>();
            jobGroupNames = _Scheduler.GetJobGroupNames().Result.ToList();            
            foreach (var groupName in jobGroupNames)
            {
                GroupMatcher<JobKey> groupMatcher = GroupMatcher<JobKey>.GroupEquals(groupName);
                var allJobKeys = _Scheduler.GetJobKeys(groupMatcher).Result;
                List <IJobDetail> details = new List<IJobDetail>();
                foreach (var jobKey in allJobKeys)
                {
                    var jobDetail = _Scheduler.GetJobDetail(jobKey).Result;

                    details.Add(jobDetail);
                }
                groupJobs.Add(groupName, details);
            }


            groupTriggers = new Dictionary<string, List<ITrigger>>();
            triggerGroupNames = _Scheduler.GetTriggerGroupNames().Result.ToList();
            foreach (var groupName in triggerGroupNames)
            {
                GroupMatcher<TriggerKey> groupMatcher = GroupMatcher<TriggerKey>.GroupEquals(groupName);
                var allTriggerKey = _Scheduler.GetTriggerKeys(groupMatcher).Result;
                List<ITrigger> triggers = new List<ITrigger>();
                foreach (var triggerKey in allTriggerKey)
                {
                    var t = _Scheduler.GetTrigger(triggerKey).Result;
                    triggers.Add(t);
                }
                groupTriggers.Add(groupName, triggers);
            }

        }





        public JsonResult OnPost() 
        {
            
            string? action = Request.Form["action"];


            // _Scheduler.p


            if (action == "remove") {
                
                string? jobName = Request.Form["jobname"];
                string? jobGroup = Request.Form["jobgroup"];

                if(!string.IsNullOrEmpty(jobName) 
                    && !string.IsNullOrEmpty(jobGroup))
                {
                    JobKey jobKey = JobKey.Create(jobName, jobGroup);
                    
                    bool deleteResult = _Scheduler.DeleteJob(jobKey).Result;

                    if (deleteResult)
                    {
                        return new JsonResult(new { success = true });
                    } else
                    {
                        return new JsonResult(new { success = false, message = "ɾ������ʧ��" });
                    }
                    
                } 
                else
                {
                    return new JsonResult(new { success = false, message = "�������㣡" });
                }

                
            }
            else if (action == "pause" || action == "resume")
            {
                // ��ͣ.
                string? jobName = Request.Form["jobname"];
                string? jobGroup = Request.Form["jobgroup"];

                if (!string.IsNullOrEmpty(jobName)
                    && !string.IsNullOrEmpty(jobGroup))
                {
                    JobKey jobKey = JobKey.Create(jobName, jobGroup);


                    if (action == "pause")
                    {
                        _Scheduler.PauseJob(jobKey);
                    } 
                    else
                    {
                        _Scheduler.ResumeJob(jobKey);
                    }
                    
                    
                    return new JsonResult(new { success = true });                    
                }
                else
                {
                    return new JsonResult(new { success = false, message = "�������㣡" });
                }
            }
            else if (action == "schedulejob")
            {
                string? jobName = Request.Form["jobname"];
                string? jobGroup = Request.Form["jobgroup"];

                string? triggerName = Request.Form["triggername"];
                string? triggerGroup = Request.Form["triggergroup"];

                

                if (!string.IsNullOrEmpty(jobName)
                    && !string.IsNullOrEmpty(jobGroup)
                    && !string.IsNullOrEmpty(triggerName)
                    && !string.IsNullOrEmpty(triggerGroup))
                {
                    JobKey jobKey = JobKey.Create(jobName, jobGroup);
                    IJobDetail? job = _Scheduler.GetJobDetail(jobKey).Result;
                    if(job == null)
                    {
                        return new JsonResult(new { success = false, message = "�޷���ȡ IJobDetail��" });
                    }

                    TriggerKey triggerKey = new TriggerKey(triggerName, triggerGroup);
                    ITrigger? trigger = _Scheduler.GetTrigger(triggerKey).Result;
                    if (trigger == null)
                    {
                        return new JsonResult(new { success = false, message = "�޷���ȡ ITrigger��" });
                    }

                    var result = _Scheduler.ScheduleJob(job, trigger).Result;

                    Console.WriteLine($"ScheduleJob Result = {result}");

                    return new JsonResult(new { success = true });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "�������㣡" });
                }
            }

            return new JsonResult(new { success = false, message = "δ֪�����" });
        }



    }
}
