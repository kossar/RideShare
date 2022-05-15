using API.DTO.v1.Models.Location;
using API.DTO.v1.Models.Vehicle;

namespace API.DTO.v1.Models.TransportOffer;

public class CreateUpdateTransportOfferModel
{
    public CreateUpdateLocationModel StartLocation { get; set; } = default!;
    public CreateUpdateLocationModel DestinationLocation { get; set; } = default!;
    public CreateUpdateVehicleModel Vehicle { get; set; } = default!;
    public int AvailableSeatCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
}