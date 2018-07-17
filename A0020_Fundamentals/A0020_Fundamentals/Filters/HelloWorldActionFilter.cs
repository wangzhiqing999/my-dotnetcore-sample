using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace A0020_Fundamentals.Filters
{
    public class HelloWorldActionFilter : IActionFilter
    {

        private ILogger _logger;
        public HelloWorldActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HelloWorldActionFilter>();
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.LogInformation("########## OnActionExecuted! ########## Action 执行完毕！");
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.LogInformation("########## OnActionExecuting! ########## Action 执行开始！");
        }
    }
}