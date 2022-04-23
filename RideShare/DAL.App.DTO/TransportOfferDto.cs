using DAL.App.DTO.Common;
using DAL.App.DTO.Identity;
using Domain.App;

namespace DAL.App.DTO;

public class TransportOfferDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public Guid StartLocationId { get; set; }
    public virtual LocationDto? StartLocation { get; set; }
    public Guid DestinationLocationId { get; set; }
    public virtual LocationDto? DestinationLocation { get; set; }
    public Guid VehicleId { get; set; }
    public virtual VehicleDto? Vehicle { get; set; }
    public int AvailableSeatCount { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsAd { get; set; }
    public ICollection<ScheduleDto>? Schedules { get; set; }
    public ICollection<TransportDto>? Transports { get; set; }
}