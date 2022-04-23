using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class VehicleMapper : BaseMapper<VehicleDto, Vehicle>
{
    public VehicleMapper(IMapper mapper) : base(mapper)
    {
    }
}