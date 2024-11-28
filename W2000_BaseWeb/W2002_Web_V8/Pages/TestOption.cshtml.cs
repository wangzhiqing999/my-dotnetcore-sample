using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using W2002_Web_V8.Options;


namespace W2002_Web_V8.Pages
{
    public class TestOptionModel : PageModel
    {

        private readonly ILogger _logger;


        public TestOptions _TestOptions { get; set; }


        public TestOptionModel(ILogger<TestOptionModel> logger, IOptions<TestOptions> options)
        {
            _logger = logger;
            _TestOptions = options.Value;
        }


        public void OnGet()
        {

            _logger.LogInformation($"Code={_TestOptions.Code}; Name={_TestOptions.Name}");
        }

    }
}
