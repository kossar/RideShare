using API.DTO.v1.Models.Location;

namespace API.DTO.v1.Models.TransportNeed;

public class CreateUpdateTransportNeedModel
{
    public CreateUpdateLocationModel StartLocation { get; set; } = default!;
    public CreateUpdateLocationModel DestinationLocation { get; set; } = default!;
    public int PersonCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
}