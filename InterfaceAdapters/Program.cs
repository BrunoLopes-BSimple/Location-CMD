using Application.DTO;
using Application.IPublisher;
using Application.IService;
using Application.Services;
using Domain.Entities;
using Domain.Factories.LocationFactory;
using Domain.IRepository;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using InterfaceAdapters.Publisher;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<ILocationService, LocationService>();

// Repositories
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

// Factories
builder.Services.AddScoped<ILocationFactory, LocationFactory>();

// Mappers
builder.Services.AddTransient<LocationDataModelConverter>();

// publisher
builder.Services.AddScoped<IMessagePublisher, MassTransitPublisher>();


builder.Services.AddAutoMapper(cfg =>
{
    // DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    // DTO
    cfg.CreateMap<Location, LocationDTO>();
});

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
    });
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location API V1");
        c.RoutePrefix = "swagger"; 
    });
}


app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    if (!env.IsEnvironment("Testing"))
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<LocationContext>();
        dbContext.Database.Migrate();
    }
}

app.Run();

public partial class Program { }
