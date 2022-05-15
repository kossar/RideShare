namespace API.DTO.v1.Models.TransportOffer;

public class TransportOfferModel : CreateUpdateTransportOfferModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}

