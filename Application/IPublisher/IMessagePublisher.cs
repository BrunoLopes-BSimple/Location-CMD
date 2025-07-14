using Domain.Interfaces;

namespace Application.IPublisher;

public interface IMessagePublisher
{
    Task PublishLocationCreatedAsync(ILocation location);
}
