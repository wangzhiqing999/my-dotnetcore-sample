using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWork.Web.Areas.MyAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/MyUser")]
    public class MyUserController : Controller
    {
    }
}