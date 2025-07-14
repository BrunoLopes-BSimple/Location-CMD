using Infrastructure.DataModels;
using Infrastructure.Repositories;

namespace Infrastructure.Tests.LocationRepositoryTests;

public class LocationRepositoryCreateTests : RepositoryTestBase
{
    [Fact]
    public async Task AlreadyExists_WhenLocationExists_ShouldReturnTrue()
    {
        // arrange
        var locationId = Guid.NewGuid();
        var locationData = new LocationDataModel { Id = locationId, Description = "some description" };

        context.Locations.Add(locationData);
        await context.SaveChangesAsync();

        var repository = new LocationRepository(context, _mapper.Object);

        // act
        var result = await repository.AlreadyExists(locationId);

        // assert
        Assert.True(result);
    }

    [Fact]
    public async Task AlreadyExists_WhenLocationDoesNotExist_ShouldReturnFalse()
    {
        // arrange
        var nonExistentId = Guid.NewGuid();
        var repository = new LocationRepository(context, _mapper.Object);

        // act
        var result = await repository.AlreadyExists(nonExistentId);

        // assert
        Assert.False(result);
    }
}
