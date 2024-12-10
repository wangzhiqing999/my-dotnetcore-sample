using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;
using Quartz.Impl.Matchers;

namespace B0254_Quartz_Web.Pages.Quartz
{
    public class TriggerKeyModel : PageModel
    {

        public IScheduler _Scheduler;

        public TriggerKeyModel(IScheduler scheduler)
        {
            _Scheduler = scheduler;
        }



        public List<TriggerKey> TriggerKeys { get; set; } = new List<TriggerKey>();


        public void OnGet(string groupName)
        {
            GroupMatcher<TriggerKey> groupMatcher = GroupMatcher<TriggerKey>.GroupEquals(groupName);

            TriggerKeys = _Scheduler.GetTriggerKeys(groupMatcher).Result.ToList();
        }
    }
}
