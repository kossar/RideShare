using API.DTO.v1.Models.Location;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class LocationMapper : BaseMapper<LocationAddModel, BLL.App.DTO.LocationDto>
{
    private readonly IMapper _mapper;
    public LocationMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.LocationDto MapToBll(LocationAddModel locationAdd)
    {
        var bllLocation = _mapper.Map<BLL.App.DTO.LocationDto>(locationAdd);
        return bllLocation;
    }
}