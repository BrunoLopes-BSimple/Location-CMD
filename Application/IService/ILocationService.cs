using Application.DTO;
using Domain.Interfaces;

namespace Application.IService;

public interface ILocationService
{
    public Task<Result<LocationDTO>> Create(CreateLocationInput dto);
    public Task<ILocation?> AddLocationReferenceAsync(LocationReference reference);
    public Task CreateRequestedLocation(Guid meetingId, string description);
}
