using AutoMapper;
using BLL.App.DTO;

namespace BLL.App.Mappers;

public class TransportOfferMapper : BaseMapper<BLL.App.DTO.TransportOfferDto, DAL.App.DTO.TransportOfferDto>
{
    public TransportOfferMapper(IMapper mapper) : base(mapper)
    {
    }

    //public IEnumerable<TransportAdListDto> TransportOffersToTransportListItem(DAL.App.DTO.TransportOfferDto transportOffers, Guid? userId)
    //{
    //    return transportOffers.Select(to => MapTransportOfferToListItem(to, userId));
    //}

    public TransportAdListDto MapTransportOfferToListItem(DAL.App.DTO.TransportOfferDto transportOffer, Guid userId = default)
    {
        var transportAdList = Mapper.Map<TransportAdListDto>(transportOffer);
        transportAdList.DestinationLocationCity = transportOffer.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportOffer.StartLocation!.City;
        transportAdList.IsTransportNeed = false;
        transportAdList.UserCanView = userId != default;
        transportAdList.PersonSeatCount = transportOffer.AvailableSeatCount;

        return transportAdList;
    }
}