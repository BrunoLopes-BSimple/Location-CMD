using Application.IPublisher;
using Domain.Interfaces;

namespace InterfaceAdapters.Tests.LocationControllerTests;

public class FakeMessagePublisher : IMessagePublisher
{
    public Task PublishLocationCreatedAsync(ILocation location)
    {
        return Task.CompletedTask;
    }
}
