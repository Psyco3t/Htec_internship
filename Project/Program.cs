using Project.Models;
using Project.Context;
using Project.Services;
using Project.Controllers;
using Project.MapperConfig;
using AutoMapper;
using Project.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<MapperProfiles>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<RolesServices>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<UsersController>();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<DataContext>("Data Source=Project.db");

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
