using API.DTO.v1.Models.TransportOffer;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class TransportOfferMapper : BaseMapper<TransportOfferModel, BLL.App.DTO.TransportOfferDto>
{
    private readonly IMapper _mapper;
    public TransportOfferMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.TransportOfferDto MapToBll(TransportOfferAddModel TransportOfferAdd)
    {
        var bllTransportOffer = _mapper.Map<BLL.App.DTO.TransportOfferDto>(TransportOfferAdd);
        return bllTransportOffer;
    }
}