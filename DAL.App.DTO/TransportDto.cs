using DAL.App.DTO.Common;
using DAL.App.DTO.Enums;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class TransportDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public Guid StartLocationId { get; set; }
    public virtual LocationDto? StartLocation { get; set; }
    public Guid DestinationLocationId { get; set; }
    public virtual LocationDto? DestinationLocation { get; set; }
    public ETransportStatus Status { get; set; }

    public ICollection<ScheduleDto>? Schedules { get; set; }
}