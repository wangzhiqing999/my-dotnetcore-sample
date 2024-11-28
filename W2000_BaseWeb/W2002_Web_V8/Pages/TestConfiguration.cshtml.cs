using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace W2002_Web_V8.Pages
{
    public class TestConfigurationModel : PageModel
    {


        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration _Configuration;

        public TestConfigurationModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }



        public string? AllowedHosts { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }



        public void OnGet()
        {

            AllowedHosts = _Configuration["AllowedHosts"];

            TestCode = _Configuration["Test:Code"];
            TestName = _Configuration["Test:Name"];
        }
    }
}
