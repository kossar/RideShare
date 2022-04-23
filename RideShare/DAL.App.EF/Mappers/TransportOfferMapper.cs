using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class TransportOfferMapper : BaseMapper<TransportOfferDto, TransportOffer>
{
    public TransportOfferMapper(IMapper mapper) : base(mapper)
    {
    }
}