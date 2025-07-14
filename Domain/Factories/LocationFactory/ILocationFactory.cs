using Domain.Entities;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.Factories.LocationFactory;

public interface ILocationFactory
{
    public ILocation Create(string description);
    public ILocation Create(Guid id, string description);
    public ILocation Create(ILocationVisitor locationVisitor);
}
