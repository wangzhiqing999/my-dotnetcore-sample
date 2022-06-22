using A0058_GrpcService.Services;

namespace A0058_GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();




            // ÔÊÐí¿çÓòµÄ´úÂë.
            app.Use(async (context, next) => {
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS,HEAD,PATCH");
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Max-Age", "100000");
                context.Response.Headers.Add("Access-Control-Expose-Headers", "Grpc-Status,Grpc-Message,Grpc-Encoding,Grpc-Accept-Encoding"); if (context.Request.Method.ToUpper() == "OPTIONS")
                {
                    return;
                }
                await next.Invoke();
            });






            app.UseGrpcWeb();


            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>().EnableGrpcWeb();



            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");



            app.Run();
        }
    }
}