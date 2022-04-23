using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class LocationMapper : BaseMapper<LocationDto, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}