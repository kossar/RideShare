using API.DTO.v1.Models.Location;
using API.DTO.v1.Models.TransportNeed;
using API.DTO.v1.Models.TransportOffer;
using API.DTO.v1.Models.Vehicle;
using AutoMapper;

namespace API.DTO.v1.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BLL.App.DTO.LocationDto, CreateUpdateLocationModel>().ReverseMap();
        CreateMap<BLL.App.DTO.VehicleDto, VehicleAddModel>().ReverseMap();
        CreateMap<BLL.App.DTO.VehicleDto, VehicleModel>().ReverseMap();

        CreateMap<BLL.App.DTO.TransportNeedDto, CreateUpdateTransportNeedModel>().ReverseMap();
        CreateMap<BLL.App.DTO.TransportNeedDto, TransportNeedModel>().ReverseMap();

        CreateMap<BLL.App.DTO.TransportOfferDto, TransportOfferAddModel>().ReverseMap();
        CreateMap<BLL.App.DTO.TransportOfferDto, TransportOfferModel>().ReverseMap();
    }
}