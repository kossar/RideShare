using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class TransportNeedMapper : BaseMapper<TransportNeedDto, TransportNeed>
{
    public TransportNeedMapper(IMapper mapper) : base(mapper)
    {
    }
}