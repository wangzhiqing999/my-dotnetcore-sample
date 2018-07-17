using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace A0020_Fundamentals.Filters
{
    public class HelloWorldResultFilter : IResultFilter
    {

        private ILogger _logger;
        public HelloWorldResultFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HelloWorldResultFilter>();
        }



        void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
        {
            _logger.LogInformation("########## OnResultExecuted! ########## Result 执行完毕！");
        }



        void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        {
            _logger.LogInformation("########## OnResultExecuting! ########## Result 执行开始！");
        }
    }
}