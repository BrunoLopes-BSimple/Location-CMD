using Domain.Factories.LocationFactory;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.LocationTests;

public class LocationFactoryTests
{
    [Fact]
    public void LocationFactory_WithValidVisitor_ShouldCreateLocation()
    {
        // arrange
        var id = Guid.NewGuid();
        var description = "some description";

        var locationVisitorDouble = new Mock<ILocationVisitor>();
        locationVisitorDouble.Setup(l => l.Id).Returns(id);
        locationVisitorDouble.Setup(l => l.Description).Returns(description);

        var factory = new LocationFactory();

        // act
        var result = factory.Create(locationVisitorDouble.Object);

        // assert
        Assert.Equal(id, result.Id);
        Assert.Equal(description, result.Description);
    }

    [Fact]
    public void LocationFactory_WhenPassingValidDescription_ShouldCreateLocation()
    {
        // arrange
        var description = "some description";
        var factory = new LocationFactory();

        // act
        var result = factory.Create(description);

        // assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(description, result.Description);
    }

    [Fact]
    public void LocationFactory_WhenPassingValidIdAndDescription_ShouldCreateLocation()
    {
        // arrange
        var id = Guid.NewGuid();
        var description = "some description";
        var factory = new LocationFactory();

        // act
        var result = factory.Create(id, description);

        // assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(description, result.Description);
    }
}
