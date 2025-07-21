using Application.IPublisher;
using Domain.Contracts;
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

    public async Task PublishRequestedLocationAsync(Guid meetingId, ILocation location)
    {
        var message = new LocationCreatedMessage(location.Id, location.Description);
        await _publishEndpoint.Publish(message);

        await _publishEndpoint.Publish(new LocationCreatedForMeeting(location.Id, meetingId));
    }
}
