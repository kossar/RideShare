using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class ScheduleMapper : BaseMapper<ScheduleDto, Schedule>
{
    public ScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}