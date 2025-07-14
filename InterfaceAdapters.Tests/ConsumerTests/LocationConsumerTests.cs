using Application.DTO;
using Application.IService;
using Domain.Messages;
using InterfaceAdapters.Consumers;
using MassTransit;
using Moq;

namespace InterfaceAdapters.Tests.ConsumerTests;

public class LocationConsumerTests
{
    [Fact]
    public async Task Consume_WhenMessageIsReceived_ShouldCallAddLocationReferenceAsyncWithCorrectData()
    {
        // arrange
        var serviceDouble = new Mock<ILocationService>();
        var consumer = new LocationCreatedConsumer(serviceDouble.Object);

        var message = new LocationCreatedMessage(Guid.NewGuid(), "some description");

        var context = Mock.Of<ConsumeContext<LocationCreatedMessage>>(c => c.Message == message);

        var reference = new LocationReference { Id = context.Message.id, Description = context.Message.description };

        // act
        await consumer.Consume(context);

        // assert
        serviceDouble.Verify(s => s.AddLocationReferenceAsync(reference), Times.Once);
    }
}
