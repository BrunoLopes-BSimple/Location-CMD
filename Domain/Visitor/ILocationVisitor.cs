namespace Domain.Visitor;

public interface ILocationVisitor
{
    Guid Id { get; }
    string Description { get; }
}
