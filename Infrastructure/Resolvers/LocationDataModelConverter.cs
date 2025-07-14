using AutoMapper;
using Domain.Entities;
using Domain.Factories.LocationFactory;
using Infrastructure.DataModels;

namespace Infrastructure.Resolvers;

public class LocationDataModelConverter : ITypeConverter<LocationDataModel, Location>
{
    public Location Convert(LocationDataModel source, Location destination, ResolutionContext context)
    {
        return new Location(source.Id, source.Description);
    }
}
