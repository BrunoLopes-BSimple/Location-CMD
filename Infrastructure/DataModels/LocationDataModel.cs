using Domain.Visitor;

namespace Infrastructure.DataModels;

public class LocationDataModel : ILocationVisitor
{
    public Guid Id { get; set; }
    public string Description { get; set; }

    public LocationDataModel(Guid id, string description)
    {
        Id = id;
        Description = description;
    }

    public LocationDataModel() { }
}
