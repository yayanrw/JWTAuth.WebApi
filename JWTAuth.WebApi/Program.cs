using JWTAuth.WebApi.Interface;
using JWTAuth.WebApi.Models;
using JWTAuth.WebApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Donot forgot to add ConnectionStrings as "dbConnection" to the appsetting.json file
string connString = builder.Configuration.GetConnectionString("dbConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseMySql(connString, new MySqlServerVersion(new Version(10, 1, 40))));
builder.Services.AddTransient<IEmployees, EmployeeRepository>();
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
