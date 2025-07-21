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
using InterfaceAdapters;
using InterfaceAdapters.Consumers;
using InterfaceAdapters.Publisher;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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
    x.AddConsumer<LocationCreatedConsumer>();
    x.AddConsumer<CreateLocationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        var instance = InstanceInfo.InstanceId;
        cfg.ReceiveEndpoint($"location-cmd-{instance}", e =>
        {
            e.ConfigureConsumers(context);
        });

        cfg.ReceiveEndpoint("data-for-location", e =>
        {
            e.ConfigureConsumer<CreateLocationConsumer>(context);
        });
    });


});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
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
