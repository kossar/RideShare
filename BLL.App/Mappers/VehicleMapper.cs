using AutoMapper;

namespace BLL.App.Mappers;

public class VehicleMapper : BaseMapper<BLL.App.DTO.VehicleDto, DAL.App.DTO.VehicleDto>
{
    public VehicleMapper(IMapper mapper) : base(mapper)
    {
    }
}