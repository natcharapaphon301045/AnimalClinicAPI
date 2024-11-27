using Microsoft.AspNetCore.Mvc;
using AnimalClinicAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (Database connection)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VetweinaryClinic")));

// Add services to the container
builder.Services.AddControllers();

// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // เปิดใช้งาน Swagger

var app = builder.Build();

// Enable Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // เปิด Swagger (API documentation)
    app.UseSwaggerUI(); // เปิด Swagger UI (Interface for testing API)
}

app.UseHttpsRedirection();

app.MapControllers(); // สร้าง routing สำหรับ Controllers

app.Run();
