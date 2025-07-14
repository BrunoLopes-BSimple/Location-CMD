using Application.DTO;
using Application.IPublisher;
using Application.Services;
using AutoMapper;
using Domain.Factories.LocationFactory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.LocationServiceTests;

public class LocationServiceCreateTests
{
    [Fact]
    public async Task Create_WithValidInput_ShouldReturnLocationDTO()
    {
        // arrange
        var id = Guid.NewGuid();
        var description = "some description";
        var createLocationInput = new CreateLocationInput { Description = description };

        var location = new Mock<ILocation>();
        location.Setup(l => l.Id).Returns(id);
        location.Setup(l => l.Description).Returns(description);

        var expectedDto = new LocationDTO { Id = id, Description = description };

        var factoryDouble = new Mock<ILocationFactory>();
        factoryDouble.Setup(f => f.Create(description)).Returns(location.Object);

        var repoDouble = new Mock<ILocationRepository>();
        repoDouble.Setup(r => r.AddAsync(location.Object)).ReturnsAsync(location.Object);

        var publisherDouble = new Mock<IMessagePublisher>();
        publisherDouble.Setup(p => p.PublishLocationCreatedAsync(location.Object)).Returns(Task.CompletedTask);

        var mapperDouble = new Mock<IMapper>();
        mapperDouble.Setup(m => m.Map<LocationDTO>(location.Object)).Returns(expectedDto);

        var service = new LocationService(repoDouble.Object, factoryDouble.Object, mapperDouble.Object, publisherDouble.Object);

        // act
        var result = await service.Create(createLocationInput);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedDto.Id, result.Id);
        Assert.Equal(expectedDto.Description, result.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Create_WithNullOrWhitespaceDescription_ShouldThrowArgumentException(string invalidDescription)
    {
        // Arrange
        var input = new CreateLocationInput { Description = invalidDescription };

        var factoryDouble = new Mock<ILocationFactory>();
        var repoDouble = new Mock<ILocationRepository>();
        var publisherDouble = new Mock<IMessagePublisher>();
        var mapperDouble = new Mock<IMapper>();

        var service = new LocationService(repoDouble.Object, factoryDouble.Object, mapperDouble.Object, publisherDouble.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.Create(input));
        factoryDouble.Verify(f => f.Create(It.IsAny<string>()), Times.Never);
    }
}
