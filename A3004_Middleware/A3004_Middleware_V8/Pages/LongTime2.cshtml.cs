using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;

namespace A3004_Middleware_V8.Pages
{

    [OutputCache(Duration = 30)]
    public class LongTime2Model : PageModel
    {

        public DateTime ProcessingTime { get; set; }


        public void OnGet()
        {
            Thread.Sleep(5000);

            ProcessingTime = DateTime.Now;
        }
    }
}
