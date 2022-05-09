using API.DTO.v1.Models.Location;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class LocationMapper : BaseMapper<CreateUpdateLocationModel, BLL.App.DTO.LocationDto>
{
    private readonly IMapper _mapper;
    public LocationMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.LocationDto MapToBll(CreateUpdateLocationModel createUpdateLocation)
    {
        var bllLocation = _mapper.Map<BLL.App.DTO.LocationDto>(createUpdateLocation);
        return bllLocation;
    }
}