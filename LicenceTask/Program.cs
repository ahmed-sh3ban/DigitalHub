using LicenceTask.Domain;
using LicenceTask.Model;
using LicenceTask.Repository;
using LicenceTask.Services;
using LicenceTask.Services.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISigning, Signing>();
builder.Services.AddScoped<ISubscriptionLicenseService, SubscriptionLicenseService>();
builder.Services.AddScoped<ISubscriptionLicenseRepository, SubscriptionLicenseRepository>();
builder.Services.Configure<AesKeys>(builder.Configuration.GetSection("AesKeys"));

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
