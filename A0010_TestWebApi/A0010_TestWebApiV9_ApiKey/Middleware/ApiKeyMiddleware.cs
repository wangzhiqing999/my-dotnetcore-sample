namespace A0010_TestWebApiV9_ApiKey.Middleware
{

    /// <summary>
    /// API 密钥身份验证中间件.
    /// </summary>
    public class ApiKeyMiddleware
    {

        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }


            // 说明：
            // 这里是简单从 appsettings.json 中读取 ApiKey，
            // 实际开发中，应从数据库或者缓存中读取，这里只是演示。
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>("ApiKey");
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }


            // 如果业务中，存在角色判断的需求，可以在这里做，例如：
            context.Items["UserRole"] = "ADMIN";

            await _next(context);
        }

    }
}
