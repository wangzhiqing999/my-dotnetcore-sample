using System;
using Avalonia;
using Microsoft.Extensions.Configuration;

namespace A5500_AvaloniaApp
{
    internal sealed class Program
    {

        // 全局静态配置，整个项目都能用
        public static IConfiguration Configuration { get; private set; }


        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // 1. 加载配置文件
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // 程序运行目录
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 启动 Avalonia 应用
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}
