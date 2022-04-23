using AutoMapper;

namespace BLL.App.Mappers;

public class TransportOfferMapper : BaseMapper<BLL.App.DTO.TransportOfferDto, DAL.App.DTO.TransportOfferDto>
{
    public TransportOfferMapper(IMapper mapper) : base(mapper)
    {
    }
}