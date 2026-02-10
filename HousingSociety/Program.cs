
using HousingSociety.Application.Interfaces;
using HousingSociety.Application.Interfaces.Repositories;
using HousingSociety.Application.Services;
using HousingSociety.Infrastructure.Persistence;
using HousingSociety.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<ISocietyRepository, SocietyRepository>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

// Services (implement these in Application if you haven't yet)
builder.Services.AddScoped<ISocietyService, SocietyService>();
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
