using API.DTO.v1.Models.Ad;
using API.DTO.v1.Models.Location;
using API.DTO.v1.Models.TransportNeed;
using API.DTO.v1.Models.TransportOffer;
using API.DTO.v1.Models.Vehicle;
using AutoMapper;
using BLL.App.DTO;

namespace API.DTO.v1.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BLL.App.DTO.LocationDto, CreateUpdateLocationModel>().ReverseMap();
        CreateMap<BLL.App.DTO.VehicleDto, CreateUpdateVehicleModel>().ReverseMap();
        CreateMap<BLL.App.DTO.VehicleDto, VehicleModel>().ReverseMap();

        CreateMap<BLL.App.DTO.TransportNeedDto, CreateUpdateTransportNeedModel>().ReverseMap();
        CreateMap<BLL.App.DTO.TransportNeedDto, TransportNeedModel>().ReverseMap();
        CreateMap<DAL.App.DTO.TransportNeedDto, TransportAdListModel>().ReverseMap();

        CreateMap<BLL.App.DTO.TransportOfferDto, CreateUpdateTransportOfferModel>().ReverseMap();
        CreateMap<BLL.App.DTO.TransportOfferDto, TransportOfferModel>().ReverseMap();
        CreateMap<DAL.App.DTO.TransportOfferDto, TransportAdListModel>().ReverseMap();


        CreateMap<TransportAdListDto, TransportAdListModel>().ReverseMap();
    }
}