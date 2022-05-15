using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers;

public class TransportMapper : BaseMapper<BLL.App.DTO.TransportDto, DAL.App.DTO.TransportDto>
{
    public TransportMapper(IMapper mapper) : base(mapper)
    {
    }

    public IEnumerable<TransportAdListDto> TransportNeedsToTransportListItem(IEnumerable<DAL.App.DTO.TransportNeedDto> transportNeeds, Guid? userId)
    {
        var dtos = transportNeeds.Select(tn => MapTransportNeedToListItem(tn, userId));
        return dtos;
    }

    public IEnumerable<TransportAdListDto> TransportOffersToTransportListItem(IEnumerable<DAL.App.DTO.TransportOfferDto> transportOffers, Guid? userId)
    {
        var dtos = transportOffers.Select(to => MapTransportOfferToListItem(to, userId));
        return dtos;
    }

    private TransportAdListDto MapTransportNeedToListItem(DAL.App.DTO.TransportNeedDto transportNeed, Guid? userId)
    {
        var transportAdList = Mapper.Map<TransportAdListDto>(transportNeed);
        //transportAdList.StartAt = transportNeed.StartAt.ToLocalTime();
        //transportAdList.CreatedAt = transportNeed.CreatedAt.ToLocalTime();
        transportAdList.DestinationLocationCity = transportNeed.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportNeed.StartLocation!.City;
        transportAdList.IsTransportNeed = true;
        transportAdList.UserCanView = userId != null;
        transportAdList.PersonSeatCount = transportNeed.PersonCount;

        return transportAdList;
    }

    private TransportAdListDto MapTransportOfferToListItem(DAL.App.DTO.TransportOfferDto transportOffer, Guid? userId)
    {
        var transportAdList = Mapper.Map<TransportAdListDto>(transportOffer);
        //transportAdList.StartAt = transportOffer.StartAt.ToLocalTime();
        //transportAdList.CreatedAt = transportOffer.CreatedAt.ToLocalTime();
        transportAdList.DestinationLocationCity = transportOffer.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportOffer.StartLocation!.City;
        transportAdList.IsTransportNeed = false;
        transportAdList.UserCanView = userId != null;
        transportAdList.PersonSeatCount = transportOffer.AvailableSeatCount;

        return transportAdList;
    }


}