using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LocationRepository : GenericRepositoryEF<ILocation, Location, LocationDataModel>, ILocationRepository
{
    private readonly IMapper _mapper;

    public LocationRepository(LocationContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> AlreadyExists(Guid locationId)
    {
        return await _context.Set<LocationDataModel>().AnyAsync(l => l.Id == locationId);
    }
}
