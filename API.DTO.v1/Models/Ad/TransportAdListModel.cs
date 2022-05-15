namespace API.DTO.v1.Models.Ad;

public class TransportAdListModel
{
    public Guid Id { get; set; }
    public string StartLocationCity { get; set; } = default!;
    public string DestinationLocationCity { get; set; } = default!;
    public int PersonSeatCount { get; set; }
    public decimal Price { get; set; }
    public bool IsTransportNeed { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool UserCanView { get; set; }
}