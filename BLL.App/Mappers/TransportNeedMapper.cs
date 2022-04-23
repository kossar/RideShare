using AutoMapper;

namespace BLL.App.Mappers;

public class TransportNeedMapper : BaseMapper<BLL.App.DTO.TransportNeedDto, DAL.App.DTO.TransportNeedDto>
{
    public TransportNeedMapper(IMapper mapper) : base(mapper)
    {
    }
}