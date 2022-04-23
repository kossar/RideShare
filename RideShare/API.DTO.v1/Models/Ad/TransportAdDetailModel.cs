namespace API.DTO.v1.Models.Ad;

public class TransportAdDetailModel
{
    public Guid Id { get; set; }
    public string StartLocationFull { get; set; } = default!;
    public string DestinationLocationFull{ get; set; } = default!;
    public int PersonSeatCount { get; set; }
    public bool IsTransportNeed { get; set; }
    public decimal Price { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime CreatedAt { get; set; }

}