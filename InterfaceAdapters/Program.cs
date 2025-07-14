using Application.DTO;
using Application.IService;
using Application.Services;
using Domain.Entities;
using Domain.Factories.LocationFactory;
using Domain.IRepository;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LocationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<ILocationService, LocationService>();

// Repositories

// Factories
builder.Services.AddScoped<ILocationFactory, LocationFactory>();

// Mappers
builder.Services.AddTransient<LocationDataModelConverter>();

builder.Services.AddAutoMapper(cfg =>
{
    // DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    // DTO
    cfg.CreateMap<Location, LocationDTO>();
});

// MassTransit




// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
