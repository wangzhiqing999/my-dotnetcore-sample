using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ModelContextProtocol.Server;
using System.ComponentModel;


namespace AI004_Stdio_MCP_Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Logging.AddConsole(consoleLogOptions =>
            {
                // Configure all logs to go to stderr
                consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
            });
            builder.Services
                .AddMcpServer()
                .WithStdioServerTransport()
                .WithToolsFromAssembly();

            await builder.Build().RunAsync();
        }
    }



    [McpServerToolType]
    public static class EchoTool
    {

        // Expose a tool that echoes the input message back to the client.
        [McpServerTool, Description("Echoes the message back to the client.")]
        public static string Echo(string message) => $"Hello from C#: {message}";


        // Expose a tool that returns the input message in reverse.
        [McpServerTool, Description("Echoes in reverse the message sent by the client.")]
        public static string ReverseEcho(string message) => new string(message.Reverse().ToArray());
    }


    [McpServerToolType]
    public static class TimeTool
    {
        [McpServerTool, Description("Get the current time for a city")]
        public static string GetCurrentTime(string city) =>
            $"It is {DateTime.Now.Hour}:{DateTime.Now.Minute} in {city}.";
    }
}
