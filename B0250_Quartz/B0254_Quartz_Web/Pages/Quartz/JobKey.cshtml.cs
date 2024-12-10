using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;
using Quartz.Impl.Matchers;

namespace B0254_Quartz_Web.Pages.Quartz
{
    public class JobKeyModel : PageModel
    {

        public IScheduler _Scheduler;

        public JobKeyModel(IScheduler scheduler)
        {
            _Scheduler = scheduler;
        }



        public List<JobKey> JobKeys { get; set; } = new List<JobKey>();



        public void OnGet(string groupName)
        {
            GroupMatcher<JobKey> groupMatcher = GroupMatcher<JobKey>.GroupEquals(groupName);
            JobKeys = _Scheduler.GetJobKeys(groupMatcher).Result.ToList();

            

        }
    }
}
