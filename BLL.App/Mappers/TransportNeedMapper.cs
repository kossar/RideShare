using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers;

public class TransportNeedMapper : BaseMapper<BLL.App.DTO.TransportNeedDto, DAL.App.DTO.TransportNeedDto>
{
    public TransportNeedMapper(IMapper mapper) : base(mapper)
    {
    }

    //public IEnumerable<TransportAdListDto> MapToTransportAdListDto(IEnumerable<TransportNeedDto> transportNeeds,
    //    Guid? userId)
    //{
    //    return transportNeeds.Select(tn => MapTransportNeedToListItem(tn, userId));
    //}

    private TransportAdListDto MapTransportNeedToListItem(DAL.App.DTO.TransportNeedDto transportNeed, Guid? userId)
    {
        var transportAdList = Mapper.Map<TransportAdListDto>(transportNeed);
        transportAdList.DestinationLocationCity = transportNeed.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportNeed.StartLocation!.City;
        transportAdList.IsTransportNeed = true;
        transportAdList.UserCanView = userId != null;
        transportAdList.PersonSeatCount = transportNeed.PersonCount;

        return transportAdList;
    }
}