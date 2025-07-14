namespace Application.DTO;

public record LocationDTO
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
}
