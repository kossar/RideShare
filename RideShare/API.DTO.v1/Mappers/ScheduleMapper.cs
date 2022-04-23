using API.DTO.v1.Models.Schedule;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class ScheduleMapper : BaseMapper<ScheduleModel, BLL.App.DTO.ScheduleDto>
{
    private readonly IMapper _mapper;
    public ScheduleMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.ScheduleDto MapToBll(ScheduleModel ScheduleAdd)
    {
        var bllSchedule = _mapper.Map<BLL.App.DTO.ScheduleDto>(ScheduleAdd);
        return bllSchedule;
    }
}