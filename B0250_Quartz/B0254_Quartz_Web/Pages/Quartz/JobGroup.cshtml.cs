using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;
using System.ComponentModel.DataAnnotations;

namespace B0254_Quartz_Web.Pages.Quartz
{
    public class JobGroupModel : PageModel
    {

        private IScheduler _Scheduler;

        public JobGroupModel(IScheduler scheduler)
        {
            _Scheduler = scheduler;
        }



        public List<string> JobGroupNames { get; set; } = new List<string>();


        public void OnGet()
        {

            JobGroupNames = _Scheduler.GetJobGroupNames().Result.ToList();

        }
    }
}
