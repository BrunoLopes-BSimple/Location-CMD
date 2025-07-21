using Domain.Interfaces;

namespace Application.IPublisher;

public interface IMessagePublisher
{
    public Task PublishLocationCreatedAsync(ILocation location);
    public Task PublishRequestedLocationAsync(Guid meetingId, ILocation location);
}
