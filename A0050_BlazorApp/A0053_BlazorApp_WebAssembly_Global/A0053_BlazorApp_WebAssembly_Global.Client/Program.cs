using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace A0053_BlazorApp_WebAssembly_Global.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var host = builder.Build();

            var logger = host.Services.GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation("Logged after the app is built in the Program file.");

            await host.RunAsync();
        }
    }
}
