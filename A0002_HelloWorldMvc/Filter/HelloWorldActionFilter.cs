using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;


namespace A0002_HelloWorldMvc.Filter
{
    public class HelloWorldActionFilter : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            Console.WriteLine("### OnActionExecuted! ###");
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Console.WriteLine("### OnActionExecuting! ###");
        }
    }
}