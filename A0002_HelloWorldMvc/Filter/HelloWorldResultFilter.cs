using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace A0002_HelloWorldMvc.Filter
{
    public class HelloWorldResultFilter : ResultFilterAttribute, IResultFilter
    {

        void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
        {
            Console.WriteLine("### OnResultExecuted! ###");
        }



        void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        {
            Console.WriteLine("### OnResultExecuting! ###");
        }
    }
}