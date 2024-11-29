using Microsoft.AspNetCore.Mvc;
using AnimalClinicAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (Database connection)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnimalClinicDB")));

// Add CORS policy to allow all origins (for testing purposes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // อนุญาตให้เข้าถึงจากทุกโดเมน
               .AllowAnyMethod() // อนุญาตทุก HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // อนุญาตทุก header
    });
});

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

// Enable CORS to allow cross-origin requests
app.UseCors("AllowAll"); // ใช้งาน CORS policy

app.UseHttpsRedirection();

app.MapControllers(); // สร้าง routing สำหรับ Controllers

app.Run();
