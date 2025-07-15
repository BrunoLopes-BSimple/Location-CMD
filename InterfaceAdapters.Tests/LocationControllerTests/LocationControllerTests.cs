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
    public async Task Create_WhenSucceeds_ThenReturns201CreatedWithLocation()
    {
        // Arrange
        var description = "some description";
        var payload = new CreateLocationDTO { Description = description };

        // act
        var response = await PostAndDeserializeAsync<LocationDTO>("/api/location", payload);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(description, response.Description);
        Assert.NotEqual(Guid.Empty, response.Id);
    }
}
