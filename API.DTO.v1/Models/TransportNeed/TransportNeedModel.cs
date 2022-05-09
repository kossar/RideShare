namespace API.DTO.v1.Models.TransportNeed;

public class TransportNeedModel : CreateUpdateTransportNeedModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}