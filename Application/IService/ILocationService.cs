using Application.DTO;
using Domain.Interfaces;

namespace Application.IService;

public interface ILocationService
{
    public Task<LocationDTO> Create(CreateLocationInput dto);
    public Task<ILocation?> AddLocationReferenceAsync(LocationReference reference);
}
