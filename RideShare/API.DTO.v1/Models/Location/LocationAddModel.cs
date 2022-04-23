namespace API.DTO.v1.Models.Location;

public class LocationAddModel
{
    public Guid? Id { get; set; }
    public string Country { get; set; } = default!;
    public string? Province { get; set; }
    public string City { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Description { get; set; }
}