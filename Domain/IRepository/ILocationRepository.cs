using Domain.Entities;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ILocationRepository : IGenericRepositoryEF<ILocation, Location, ILocationVisitor>
{

}
