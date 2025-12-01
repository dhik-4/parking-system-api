using Microsoft.EntityFrameworkCore;
using ParkingSystemAPI.CustomModels;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;
using ParkingSystemAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var service = builder.Services;
string koneksiDB = builder.Configuration.GetConnectionString("DefaultConnection");// "server=localhost;port=3306;user=root;password=password1;database=book_test;";
service.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(koneksiDB,
     options => options.EnableRetryOnFailure(
        maxRetryCount: 3,
        maxRetryDelay: System.TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)
    ));
service.AddDbContext<CustomAppDbContext>(opt => opt.UseSqlServer(koneksiDB,
     options => options.EnableRetryOnFailure(
        maxRetryCount: 3,
        maxRetryDelay: System.TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)
    ));

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IFareRepository, FareRepository>();

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
