using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace A3004_Middleware_V8.Pages
{

    [ResponseCache(VaryByHeader = "Accept-Encoding", Duration = 30)]
    public class LongTimeModel : PageModel
    {        


        public DateTime ProcessingTime { get; set; }


        public void OnGet()
        {
            Thread.Sleep(5000);

            ProcessingTime = DateTime.Now;
        }
    }
}
