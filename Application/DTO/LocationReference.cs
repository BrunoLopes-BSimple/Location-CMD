namespace Application.DTO;

public record LocationReference
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
}
