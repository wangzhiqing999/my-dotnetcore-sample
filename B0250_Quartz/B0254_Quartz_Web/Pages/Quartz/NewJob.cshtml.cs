using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;
using System.ComponentModel.DataAnnotations;

using B0254_Quartz_Web.Jobs;
using Quartz.Impl.Matchers;


namespace B0254_Quartz_Web.Pages.Quartz
{
    public class NewJobModel : PageModel
    {


        public IScheduler _Scheduler;

        public NewJobModel(IScheduler scheduler)
        {
            _Scheduler = scheduler;
        }

        [BindProperty]
        [Required]
        public string JobType { set; get; }

        [BindProperty]
        [Required]
        public string JobName { set; get; }


        [BindProperty]
        public string? JobData { set; get; }


        [BindProperty]
        public string? JobDesc { set; get; }


        [BindProperty]
        public string TriggerName { set; get; } = "zeroTrigger";

        [BindProperty]
        public string? TriggerDesc { set; get; } = "使用 Cron 表达式， 时间在 0秒 的时候触发。";

        [BindProperty]
        public string TriggerCron { set; get; } = "0 * * * * ? *";







        public void OnGet()
        {
        }








        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (string.IsNullOrEmpty(JobName))
            {
                ModelState.AddModelError(string.Empty, "JobName 不能为空.");
                return Page();
            }
            if (string.IsNullOrEmpty(TriggerName))
            {
                ModelState.AddModelError(string.Empty, "TriggerName 不能为空.");
                return Page();
            }


            


            JobBuilder jobBuilder;

            if (JobType == "HelloJob")
            {
                jobBuilder = JobBuilder.Create<HelloJob>();
            }
            else if (JobType == "TestParamJob")
            {
                jobBuilder = JobBuilder.Create<TestParamJob>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "JobType 错误.");
                return Page();
            }




            JobDataMap jobDataMap = new JobDataMap();
            if(!string.IsNullOrEmpty(JobData))
            {
                string[] keyValues= JobData.Split("&");
                foreach (string kv in keyValues)
                {
                    string[] kv2 = kv.Split("=");
                    if (kv2 != null && kv2.Length == 2)
                    {
                        jobDataMap.Add(kv2[0], kv2[1]);
                    }
                }
            }


            IJobDetail job = jobBuilder
                .WithIdentity(JobName)
                .WithDescription(JobDesc)
                .UsingJobData(jobDataMap)
                .Build();




            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(TriggerName)
                .WithDescription(TriggerDesc)
                .WithCronSchedule(TriggerCron)
                .Build();
            
            try
            {
                var result = _Scheduler.ScheduleJob(job, trigger).Result;                
            } 
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("/Quartz/JobGroup");
        }


    }
}
