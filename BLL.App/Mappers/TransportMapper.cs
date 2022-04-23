using AutoMapper;

namespace BLL.App.Mappers;

public class TransportMapper : BaseMapper<BLL.App.DTO.TransportDto, DAL.App.DTO.TransportDto>
{
    public TransportMapper(IMapper mapper) : base(mapper)
    {
    }
}