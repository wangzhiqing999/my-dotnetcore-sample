using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace W2002_Web_V8.Pages
{
    public class TestSessionModel : PageModel
    {

        public const string SessionKeyName = "Session_Name";
        public const string SessionKeyAge = "Session_Age";


        public string? nameValue;
        public int? ageValue;


        public void OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                nameValue = HttpContext.Session.GetString(SessionKeyName);
                ageValue = HttpContext.Session.GetInt32(SessionKeyAge);
            }
        }


        public void OnPost()
        {

            if (Request.Form.TryGetValue("name", out var name))
            {
                nameValue = name;
                HttpContext.Session.SetString(SessionKeyName, nameValue);
            }

            if (Request.Form.TryGetValue("age", out var age))
            {
                ageValue = int.Parse(age);
                HttpContext.Session.SetInt32(SessionKeyAge, ageValue.Value);
            }

        }


        


    }
}
