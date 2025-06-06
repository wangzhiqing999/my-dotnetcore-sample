﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using W4002_BootstrapBlazorWasm;
using W4002_BootstrapBlazorWasm.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// 增加 BootstrapBlazor 组件
builder.Services.AddBootstrapBlazor(op =>
{
    // 设置组件默认使用中文
    op.DefaultCultureInfo = "zh-CN";
    op.IgnoreLocalizerMissing = true;
});

builder.Services.AddSingleton<WeatherForecastService>();

// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

await builder.Build().RunAsync();
