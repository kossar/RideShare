using AutoMapper;

namespace BLL.App.Mappers;

public class ScheduleMapper : BaseMapper<BLL.App.DTO.ScheduleDto, DAL.App.DTO.ScheduleDto>
{
    public ScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}