using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Helpers;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.impl;
using Westcoast_Education_Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddDbContext<ApplicationContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"))
// );

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(
    options =>
    {
        options.User.RequireUniqueEmail = true;
    }
).AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// builder.Services.AddCors(options =>
// {
//   options.AddPolicy("WestcoastCors",
//     policy =>
//     {
//       policy.AllowAnyHeader();
//       policy.AllowAnyMethod();
//       policy.WithOrigins(
//         "http://127.0.0.1:5500",
//         "http://localhost:3000");
//     }
//   );
// });

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

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
