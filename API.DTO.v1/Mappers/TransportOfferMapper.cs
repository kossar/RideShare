using API.DTO.v1.Models.TransportOffer;
using AutoMapper;
using BLL.App.DTO;

namespace API.DTO.v1.Mappers;

public class TransportOfferMapper : BaseMapper<TransportOfferModel, BLL.App.DTO.TransportOfferDto>
{
    private readonly IMapper _mapper;
    public TransportOfferMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.TransportOfferDto MapToBll(CreateUpdateTransportOfferModel TransportOfferAdd, Guid userId)
    {
        var bllTransportOffer = _mapper.Map<BLL.App.DTO.TransportOfferDto>(TransportOfferAdd);
        bllTransportOffer.StartLocation!.UserId = userId;
        bllTransportOffer.DestinationLocation!.UserId = userId;
        bllTransportOffer.Vehicle!.UserId = userId;
        bllTransportOffer.UserId = userId;
        return bllTransportOffer;
    }
    public TransportAdListDto MapTransportOfferToListItem(DAL.App.DTO.TransportOfferDto transportOffer, Guid? userId)
    {
        var transportAdList = Mapper.Map<TransportAdListDto>(transportOffer);
        //transportAdList.CreatedAt = transportOffer.CreatedAt.ToLocalTime();
        //transportAdList.StartAt = transportOffer.StartAt.ToLocalTime();
        transportAdList.DestinationLocationCity = transportOffer.DestinationLocation!.City;
        transportAdList.StartLocationCity = transportOffer.StartLocation!.City;
        transportAdList.IsTransportNeed = false;
        transportAdList.UserCanView = userId != null;
        transportAdList.PersonSeatCount = transportOffer.AvailableSeatCount;

        return transportAdList;
    }
}