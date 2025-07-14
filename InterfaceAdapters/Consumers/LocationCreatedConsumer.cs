using Application.DTO;
using Application.IService;
using Domain.Messages;
using MassTransit;

namespace InterfaceAdapters.Consumers;

public class LocationCreatedConsumer : IConsumer<LocationCreatedMessage>
{
    private readonly ILocationService _locationService;

    public LocationCreatedConsumer(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task Consume(ConsumeContext<LocationCreatedMessage> context)
    {
        var locationReference = new LocationReference { Id = context.Message.id, Description = context.Message.description };
        
        await _locationService.AddLocationReferenceAsync(locationReference);
    }
}
