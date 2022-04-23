namespace API.DTO.v1.Models.TransportOffer;

public class TransportOfferAddModel
{
    public Guid StartLocationId { get; set; }
    public Guid DestinationLocationId { get; set; }
    public Guid VehicleId { get; set; }
    public int AvailableSeatCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
}