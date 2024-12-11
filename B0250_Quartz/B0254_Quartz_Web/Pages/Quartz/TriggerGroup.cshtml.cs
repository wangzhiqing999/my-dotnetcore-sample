using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;

namespace B0254_Quartz_Web.Pages.Quartz
{
    public class TriggerGroupModel : PageModel
    {


        private IScheduler _Scheduler;

        public TriggerGroupModel(ISchedulerFactory schedulerFactory)
        {
            _Scheduler = schedulerFactory.GetScheduler().Result;
        }

        public List<string> TriggerGroupNames { get; set; } = new List<string>();

        public void OnGet()
        {
            this.TriggerGroupNames = _Scheduler.GetTriggerGroupNames().Result.ToList();
        }
    }
}
