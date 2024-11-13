using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace A0053_BlazorApp_Auto_Global.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            await builder.Build().RunAsync();
        }
    }
}
