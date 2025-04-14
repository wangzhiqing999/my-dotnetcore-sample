namespace AI004_MCP_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Starting MCP Server...");


                var builder = WebApplication.CreateBuilder(args);


                builder.Services.AddMcpServer().WithToolsFromAssembly();


                var app = builder.Build();

                app.MapGet("/", () => "Hello MCP Server!");
                app.MapMcp();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Host terminated unexpectedly : {ex.Message}");
            }

        }
    }
}
