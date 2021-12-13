using D0010_MyTodo.DataAccess;
using D0010_MyTodo.Service;
using D0010_MyTodo.ServiceImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<MyTodoContext>(opt => opt.UseSqlite(@"Data Source=data/todo.db"));

builder.Services.AddScoped(typeof(ITodoService), typeof(DefaultTodoService));




builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
