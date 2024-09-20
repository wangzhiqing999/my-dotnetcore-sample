using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace A0010_TestWebApiV8.ErrorHandler
{

    /// <summary>
    /// 全局异常处理.
    /// </summary>
    public class GlobalExceptionHanlder : IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHanlder> _logger;
        public GlobalExceptionHanlder(ILogger<GlobalExceptionHanlder> logger)
        {
            _logger = logger;
        }

        private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var problemDetails = CreateProblemDetails(httpContext, exception);
            var jsonDetails = ConvertToJson(problemDetails);
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsync(jsonDetails, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = UnhandledExceptionMsg;
            }

            var problemDetails = new ProblemDetails
            {
                Detail = $"API Error {exception.Message}",
                Instance = "API",
                Status = statusCode,
                Title = reasonPhrase,

            };

            return problemDetails;
        }

        private string ConvertToJson(in ProblemDetails problemDetails)
        {
            try
            {
                return JsonSerializer.Serialize(problemDetails);
            }
            catch (Exception ex)
            {
                const string msg = "An exception has occurred while serializing error to JSON";
                _logger.LogError(msg, ex);
            }

            return string.Empty;
        }
    }
}
