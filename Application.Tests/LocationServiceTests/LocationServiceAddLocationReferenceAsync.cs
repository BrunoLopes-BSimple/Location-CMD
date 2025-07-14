using Application.DTO;
using Application.IPublisher;
using Application.Services;
using AutoMapper;
using Domain.Factories.LocationFactory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.LocationServiceTests;

public class LocationServiceAddLocationReferenceAsync
{
    [Fact]
    public async Task AddLocationReferenceAsync_WhenLocationAlreadyExists_ShouldReturnNullAndNotCreate()
    {
        // arrange
        var id = Guid.NewGuid();
        var description = "some description";
        var reference = new LocationReference { Id = id, Description = description };

        var factoryDouble = new Mock<ILocationFactory>();

        var repoDouble = new Mock<ILocationRepository>();
        repoDouble.Setup(r => r.AlreadyExists(reference.Id)).ReturnsAsync(true);

        var publisherDouble = new Mock<IMessagePublisher>();
        var mapperDouble = new Mock<IMapper>();


        var service = new LocationService(repoDouble.Object, factoryDouble.Object, mapperDouble.Object, publisherDouble.Object);


        // act
        var result = await service.AddLocationReferenceAsync(reference);

        // assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddLocationReferenceAsync_WhenLocationDoesNotExist_ShouldCreateAndAddLocation()
    {
        // arrange
        var id = Guid.NewGuid();
        var description = "some description";
        var reference = new LocationReference { Id = id, Description = description };

        var locationDouble = new Mock<ILocation>();
        locationDouble.Setup(l => l.Id).Returns(id);
        locationDouble.Setup(l => l.Description).Returns(description);

        var repoDouble = new Mock<ILocationRepository>();
        repoDouble.Setup(r => r.AlreadyExists(reference.Id)).ReturnsAsync(false);
        repoDouble.Setup(r => r.AddAsync(locationDouble.Object)).ReturnsAsync(locationDouble.Object);


        var factoryDouble = new Mock<ILocationFactory>();
        factoryDouble.Setup(f => f.Create(id, description)).Returns(locationDouble.Object);

        var publisherDouble = new Mock<IMessagePublisher>();
        var mapperDouble = new Mock<IMapper>();


        var service = new LocationService(repoDouble.Object, factoryDouble.Object, mapperDouble.Object, publisherDouble.Object);


        // act
        var result = await service.AddLocationReferenceAsync(reference);

        // assert
        Assert.NotNull(result);
        Assert.Same(locationDouble.Object, result);
    }
}
