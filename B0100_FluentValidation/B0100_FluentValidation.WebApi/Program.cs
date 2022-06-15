using FluentValidation;
using FluentValidation.AspNetCore;

using B0100_FluentValidation.Model;
using B0100_FluentValidation.Validator;


namespace B0100_FluentValidation.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // 修改了下面的代码. 注入验证器.
            builder.Services.AddControllers().AddFluentValidation();
            builder.Services.AddTransient<IValidator<UserInformation>, UserInformationValidator>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}