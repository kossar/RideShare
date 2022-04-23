using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class TransportMapper : BaseMapper<TransportDto, Transport>
{
    public TransportMapper(IMapper mapper) : base(mapper)
    {
    }
}