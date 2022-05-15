using AutoMapper;

namespace BLL.App.DTO.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TransportDto, DAL.App.DTO.TransportDto>().ReverseMap();

        CreateMap<TransportNeedDto, DAL.App.DTO.TransportNeedDto>().ReverseMap();
        CreateMap<TransportNeedDto, TransportAdListDto>().ReverseMap();
        CreateMap<DAL.App.DTO.TransportNeedDto, TransportAdListDto>().ReverseMap();

        CreateMap<TransportOfferDto, DAL.App.DTO.TransportOfferDto>().ReverseMap();
        CreateMap<TransportOfferDto, TransportAdListDto>().ReverseMap();
        CreateMap<DAL.App.DTO.TransportOfferDto, TransportAdListDto>().ReverseMap();

        CreateMap<LocationDto, DAL.App.DTO.LocationDto>().ReverseMap();

        CreateMap<VehicleDto, DAL.App.DTO.VehicleDto>().ReverseMap();

        CreateMap<ScheduleDto, DAL.App.DTO.ScheduleDto>().ReverseMap();

        CreateMap<QuestionAnswerDto, DAL.App.DTO.QuestionAnswerDto>().ReverseMap();
    }
}