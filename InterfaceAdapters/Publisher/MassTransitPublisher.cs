using Application.IPublisher;
using Domain.Interfaces;
using Domain.Messages;
using MassTransit;

namespace InterfaceAdapters.Publisher;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishLocationCreatedAsync(ILocation location)
    {
        var message = new LocationCreatedMessage(location.Id, location.Description);
        await _publishEndpoint.Publish(message);
    }
}
