namespace API.DTO.v1.Models.TransportNeed;

public class TransportNeedModel : TransportNeedAddModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}