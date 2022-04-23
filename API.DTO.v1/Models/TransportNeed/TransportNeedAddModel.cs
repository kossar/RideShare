namespace API.DTO.v1.Models.TransportNeed;

public class TransportNeedAddModel
{
    public Guid StartLocationId { get; set; }
    public Guid DestinationLocationId { get; set; }
    public int PersonCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
}