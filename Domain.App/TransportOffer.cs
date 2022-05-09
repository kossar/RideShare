using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class TransportOffer : DomainEntity, IDomainUserId, IDomainUser<User>
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public Guid StartLocationId { get; set; }
    public virtual Location? StartLocation { get; set; }
    public Guid DestinationLocationId { get; set; }
    public virtual Location? DestinationLocation { get; set; }
    public Guid? ScheduleId { get; set; }
    public virtual Schedule? Schedule { get; set; }
    public Guid VehicleId { get; set; }
    public virtual Vehicle? Vehicle { get; set; }
    public int AvailableSeatCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAd { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }

    public ICollection<Schedule>? Schedules { get; set; }
    public ICollection<Transport>? Transports { get; set; }
}