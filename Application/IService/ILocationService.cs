using Application.DTO;

namespace Application.IService;

public interface ILocationService
{
    public Task<LocationDTO> Create(CreateLocationInput dto);
}
