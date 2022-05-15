using BLL.App.DTO.Common;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO;

public class TransportNeedDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public Guid StartLocationId { get; set; }
    public virtual LocationDto? StartLocation { get; set; }
    public Guid DestinationLocationId { get; set; }
    public virtual LocationDto? DestinationLocation { get; set; }
    public Guid? ScheduleId { get; set; }
    public virtual ScheduleDto? Schedule { get; set; }
    public decimal Price { get; set; }
    public int PersonCount { get; set; }
    public string? Description { get; set; }
    public bool IsAd { get; set; }
    public DateTime StartAt { get; set; }
}