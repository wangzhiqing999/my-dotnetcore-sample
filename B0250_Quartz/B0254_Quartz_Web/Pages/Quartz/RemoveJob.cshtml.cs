using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quartz;

namespace B0254_Quartz_Web.Pages.Quartz
{
    public class RemoveJobModel : PageModel
    {

        public IScheduler _Scheduler;

        public RemoveJobModel(IScheduler scheduler)
        {
            _Scheduler = scheduler;
        }


        [BindProperty]
        public string Name { set; get; }

        [BindProperty]
        public string Group { set; get; }


        public void OnGet(string name, string group)
        {
            Name = name;
            Group = group;
        }




        public IActionResult OnPost()
        {

            if (string.IsNullOrEmpty(Name))
            {
                ModelState.AddModelError(string.Empty, "Name ����Ϊ��.");
                return Page();
            }
            if (string.IsNullOrEmpty(Group))
            {
                ModelState.AddModelError(string.Empty, "Group ����Ϊ��.");
                return Page();
            }



            
            JobKey jobKey = JobKey.Create(Name, Group);

            bool deleteResult = _Scheduler.DeleteJob(jobKey).Result;

            if (deleteResult)
            {
                return RedirectToPage("/Quartz/JobGroup");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "ɾ������ʧ��.");
                return Page();                
            }

            
        }

    }
}
