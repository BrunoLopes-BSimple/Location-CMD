using Domain.Entities;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.Factories.LocationFactory;

public class LocationFactory : ILocationFactory
{
    public LocationFactory(){}

    public ILocation Create(ILocationVisitor locationVisitor)
    {
        return new Location(locationVisitor.Id, locationVisitor.Description);
    }

    public ILocation Create(string description)
    {
        return new Location(description);
    }

    public ILocation Create(Guid id, string description)
    {
        return new Location(id, description);
    }
}
