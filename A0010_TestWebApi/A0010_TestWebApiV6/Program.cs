using A0010_TestWebApiV6;
using A0010_TestWebApiV6.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// 使用内存数据库进行测试.
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// ##### Swagger 相关配置 #####
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "A0010_TestWebApiV5", Version = "v1" });




    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });



    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});





// #####  JWT 相关配置. #####

// 将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// 由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
// 将配置绑定到JwtSettings实例中
var jwtSettings = new JwtSettings();
builder.Configuration.Bind("JwtSettings", jwtSettings);

builder.Services.AddAuthentication(options => {
    //认证middleware配置
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o => {
    //主要是jwt  token参数设置
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //Token颁发机构
        ValidIssuer = jwtSettings.Issuer,
        //颁发给谁
        ValidAudience = jwtSettings.Audience,
        //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ValidateIssuerSigningKey = true,
        //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
        ValidateLifetime = true,
        //允许的服务器时间偏移量
        ClockSkew = TimeSpan.Zero
    };
});










var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// 这个必须添加在app.UseAuthorization();前面，两个名称很像，注意区别。
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
