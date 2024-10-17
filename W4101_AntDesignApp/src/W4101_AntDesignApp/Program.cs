using W4101_AntDesignApp.Client.Pages;
using W4101_AntDesignApp.Components;
using AntDesign.ProLayout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAntDesign();

builder.Services.AddScoped(sp =>
{
    var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
    if (httpContext != null)
    {
        return new HttpClient
        {
            BaseAddress = new Uri(httpContext.Request.Scheme + "://" + httpContext.Request.Host)
        };
    }
    return new HttpClient();
});

W4101_AntDesignApp.Program.AddClientServices(builder.Services);

builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(W4101_AntDesignApp.Client._Imports).Assembly);

app.Run();
