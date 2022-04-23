using AutoMapper;

namespace BLL.App.Mappers;

public class LocationMapper : BaseMapper<BLL.App.DTO.LocationDto, DAL.App.DTO.LocationDto>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}