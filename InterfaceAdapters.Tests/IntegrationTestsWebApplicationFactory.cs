using Application.IPublisher;
using Infrastructure;
using InterfaceAdapters.Tests.LocationControllerTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace InterfaceAdapters.Tests;

public class IntegrationTestsWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithDatabase("testdb")
        .WithUsername("testuser")
        .WithPassword("testpass")
        .WithImage("postgres:15-alpine")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<LocationContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Register AbsanteeContext with container's connection string
            services.AddDbContext<LocationContext>(options =>
                options.UseNpgsql(_postgres.GetConnectionString()));

            // Ensure database is created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LocationContext>();
            db.Database.EnsureCreated();

            // Replace the real publisher with a fake one for testing
            services.RemoveAll<IMessagePublisher>();
            services.AddSingleton<IMessagePublisher, FakeMessagePublisher>();
        });
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgres.StopAsync();

    }
}
