using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BootstrapBlazor.Components;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


using P0002_MyEtf.DataAccess;
using P0002_MyEtf.Service;
using P0002_MyEtf.ServiceImpl;
using P0002_MyEtf.Model;
using P0002_MyEtf.ServiceModel;


using P0002_MyGrid.DataAccess;
using P0002_MyGrid.Service;
using P0002_MyGrid.ServiceImpl;
using P0002_MyGrid.ServiceModel;


using P0002_MyTrading.DataAccess;
using P0002_MyTrading.Service;
using P0002_MyTrading.ServiceImpl;



namespace P0002_MyGrid.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();


            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();



            // 数据库.
            builder.Services.AddDbContext<MyEtfContext>(opt => opt.UseNpgsql(builder.Configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_etf")));

            builder.Services.AddDbContext<MyGridContext>(opt => opt.UseNpgsql(builder.Configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_grid")));

            builder.Services.AddDbContext<MyTradingContext>(opt => opt.UseNpgsql(builder.Configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_trading")));



            // 服务注入.
            builder.Services.AddTransient<IEtfMasterService, DefaultEtfMasterService>();
            builder.Services.AddTransient<IEtfDayService, DefaultEtfDayService>();
            builder.Services.AddTransient<IEtfWeekService, DefaultEtfWeekService>();

            builder.Services.AddTransient<IGridService, DefaultGridService>();

            builder.Services.AddTransient<IHoldingService, DefaultHoldingService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();


            app.UseBootstrapBlazor();


            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}