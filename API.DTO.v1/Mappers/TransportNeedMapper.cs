using API.DTO.v1.Models.TransportNeed;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class TransportNeedMapper : BaseMapper<TransportNeedModel, BLL.App.DTO.TransportNeedDto>
{
    private readonly IMapper _mapper;
    public TransportNeedMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.TransportNeedDto MapToBll(TransportNeedModel transportNeedAdd)
    {
        var bllTransportNeed = _mapper.Map<BLL.App.DTO.TransportNeedDto>(transportNeedAdd);
        return bllTransportNeed;
    }

    public BLL.App.DTO.TransportNeedDto MapToBll(TransportNeedAddModel transportNeedAdd)
    {
        var bllTransportNeed = _mapper.Map<BLL.App.DTO.TransportNeedDto>(transportNeedAdd);
        return bllTransportNeed;
    }
}