using API.DTO.v1.Models.Location;

namespace API.DTO.v1.Models.Ad;

public class CreateUpdateTransportAdModel
{
    public CreateUpdateLocationModel StartLocation { get; set; } = default!;
    public CreateUpdateLocationModel DestinationLocation { get; set; } = default!;
    public int PersonSeatCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
}