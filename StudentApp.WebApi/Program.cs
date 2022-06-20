using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentApp.DAL;
using StudentApp.DAL.Abstract;
using StudentApp.DAL.Repositories;
using StudentApp.Service.Abstract;
using StudentApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IWeekendService, WeekendService>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IWeekendRepository, WeekendRepository>();
builder.Services.AddDbContext<StudentContext>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(
    options => {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        b =>
        {
            b.AllowAnyOrigin();
            b.AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<StudentContext>();
await db?.Database.MigrateAsync()!;
app.Run();