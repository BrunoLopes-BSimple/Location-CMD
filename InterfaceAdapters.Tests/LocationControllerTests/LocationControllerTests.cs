using System.Net;
using System.Net.Http.Json;
using Application.DTO;

namespace InterfaceAdapters.Tests.LocationControllerTests;

public class LocationControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    private readonly IntegrationTestsWebApplicationFactory<Program> _factory;

    public LocationControllerTests(IntegrationTestsWebApplicationFactory<Program> factory) : base(factory.CreateClient())
    {
        _factory = factory;
    }

    [Fact]
    public async Task Create_WhenSucceeds_ShouldReturn200OkWithLocation()
    {
        // Arrange
        var description = "some description";
        var payload = new CreateLocationDTO { Description = description };

        // Act
        var response = await PostAsync("/api/location", payload);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); 

        var responseDto = await response.Content.ReadFromJsonAsync<LocationDTO>();
        Assert.NotNull(responseDto);
        Assert.Equal(description, responseDto.Description);
        Assert.NotEqual(Guid.Empty, responseDto.Id);
    }

    [Fact]
    public async Task Create_WithInvalidInput_ShouldReturn400BadRequest()
    {
        // Arrange
        var payload = new CreateLocationDTO { Description = "" };

        // Act
        var response = await PostAsync("/api/location", payload);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.Equal("Invalid location input.", errorMessage);
    }
}
