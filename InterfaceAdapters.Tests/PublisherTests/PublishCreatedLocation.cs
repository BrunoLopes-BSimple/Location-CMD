using Domain.Interfaces;
using Domain.Messages;
using InterfaceAdapters.Publisher;
using MassTransit;
using Moq;

namespace InterfaceAdapters.Tests.PublisherTests;

public class PublishCreatedLocation
{
    [Fact]
    public async Task PublishLocationCreatedAsync_ShouldPublishEventWithCorrectData()
    {
        // Arrange 
        var publishEndpointDouble = new Mock<IPublishEndpoint>();

        var publisher = new MassTransitPublisher(publishEndpointDouble.Object);

        var locationDouble = new Mock<ILocation>();
        var locationId = Guid.NewGuid();
        var description = "some description";

        locationDouble.Setup(c => c.Id).Returns(locationId);
        locationDouble.Setup(c => c.Description).Returns(description);

        // Act 
        await publisher.PublishLocationCreatedAsync(locationDouble.Object);

        // Assert
        publishEndpointDouble.Verify(
            p => p.Publish(
                It.Is<LocationCreatedMessage>(e =>
                    e.id == locationId &&
                    e.description == description
                ),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }

}
