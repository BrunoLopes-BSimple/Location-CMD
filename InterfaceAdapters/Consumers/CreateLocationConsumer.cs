using Application.IService;
using Domain.Contracts;
using MassTransit;

namespace InterfaceAdapters.Consumers;

public class CreateLocationConsumer : IConsumer<DataForLocation>
{
    private readonly ILocationService _locationService;

    public CreateLocationConsumer(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task Consume(ConsumeContext<DataForLocation> context)
    {
        Console.WriteLine($"Received from meeting - {context.Message.description} - {context.Message.meetingId}");
        var command = context.Message;
        await _locationService.CreateRequestedLocation(command.meetingId, command.description);
    }
}

