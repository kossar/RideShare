using API.DTO.v1.Models.Ad;
using API.DTO.v1.Models.TransportNeed;
using AutoMapper;
using BLL.App.DTO;

namespace API.DTO.v1.Mappers;

public class TransportNeedMapper : BaseMapper<TransportNeedModel, BLL.App.DTO.TransportNeedDto>
{
    private readonly IMapper _mapper;
    public TransportNeedMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.TransportNeedDto MapToBll(TransportNeedModel transportNeedAdd)
    {
        var bllTransportNeed = _mapper.Map<BLL.App.DTO.TransportNeedDto>(transportNeedAdd);
        return bllTransportNeed;
    }

    public BLL.App.DTO.TransportNeedDto MapToBll(CreateUpdateTransportNeedModel transportNeedAdd, Guid userId)
    {
        var bllTransportNeed = _mapper.Map<BLL.App.DTO.TransportNeedDto>(transportNeedAdd);
        bllTransportNeed.UserId = userId;
        bllTransportNeed.StartLocation!.UserId = userId;
        bllTransportNeed.DestinationLocation!.UserId = userId;
        return bllTransportNeed;
    }

    public TransportAdListModel MapTransportNeedToAdListItem(DAL.App.DTO.TransportNeedDto transportNeed, Guid userId = default)
    {
        var transportAdList = Mapper.Map<TransportAdListModel>(transportNeed);
        //transportAdList.StartAt = transportNeed.StartAt.ToLocalTime();
        //transportAdList.CreatedAt = transportNeed.CreatedAt.ToLocalTime();
        transportAdList.DestinationLocationCity = transportNeed.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportNeed.StartLocation!.City;
        transportAdList.IsTransportNeed = true;
        transportAdList.UserCanView = userId != default;
        transportAdList.PersonSeatCount = transportNeed.PersonCount;

        return transportAdList;
    }
}