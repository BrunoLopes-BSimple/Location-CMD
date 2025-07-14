using AutoMapper;
using Domain.Entities;
using Infrastructure.DataModels;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<Location, LocationDataModel>();
        CreateMap<LocationDataModel, Location>().ConvertUsing<LocationDataModelConverter>();
    }
}
