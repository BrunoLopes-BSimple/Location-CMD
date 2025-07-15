using Application.DTO;
using Application.IPublisher;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Factories.LocationFactory;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepo;
    private readonly ILocationFactory _locationFactory;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;

    public LocationService(ILocationRepository repository, ILocationFactory factory, IMapper mapper, IMessagePublisher publisher)
    {
        _locationRepo = repository;
        _locationFactory = factory;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Result<LocationDTO>> Create(CreateLocationInput dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Description))
            return Result<LocationDTO>.Failure(Error.BadRequest("Invalid location input."));

        try
        {
            var location = _locationFactory.Create(dto.Description);
            await _locationRepo.AddAsync(location);

            await _publisher.PublishLocationCreatedAsync(location);
            var result = _mapper.Map<LocationDTO>(location);
            return Result<LocationDTO>.Success(result);
        }
        catch (Exception e)
        {
            return Result<LocationDTO>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<ILocation?> AddLocationReferenceAsync(LocationReference reference)
    {
        var locationAlreadyExists = await _locationRepo.AlreadyExists(reference.Id);
        if (locationAlreadyExists) return null;

        var newLocation = _locationFactory.Create(reference.Id, reference.Description);
        return await _locationRepo.AddAsync(newLocation);
    }
}
