using Application.DTO;
using Application.IPublisher;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Factories.LocationFactory;
using Domain.IRepository;

namespace Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _repository;
    private readonly ILocationFactory _factory;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;

    public LocationService(ILocationRepository repository, ILocationFactory factory, IMapper mapper, IMessagePublisher publisher)
    {
        _repository = repository;
        _factory = factory;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<LocationDTO> Create(CreateLocationInput dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("Invalid location input.");

        var location = _factory.Create(dto.Description);
        await _repository.AddAsync(location);

        await _publisher.PublishLocationCreatedAsync(location);
        return _mapper.Map<LocationDTO>(location);
    }
}
