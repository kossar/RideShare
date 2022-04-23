using AutoMapper;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.DTO.MappingProfiles;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TransportDto, Transport>().ReverseMap();
        CreateMap<TransportOfferDto, TransportOffer>().ReverseMap();
        CreateMap<TransportNeedDto, TransportNeed>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<ScheduleDto, Schedule>().ReverseMap();
        CreateMap<VehicleDto, Vehicle>().ReverseMap();
        CreateMap<QuestionAnswerDto, QuestionAnswer>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<UserRoleDto, UserRole>().ReverseMap();
    }
}