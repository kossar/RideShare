using Contracts.Domain.Base;
using Domain.App.Enums;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Transport : DomainEntity, IDomainUserId, IDomainUser<User>
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public Guid StartLocationId { get; set; }
    public virtual Location? StartLocation { get; set; }
    public Guid DestinationLocationId { get; set; }
    public virtual Location? DestinationLocation { get; set; }

    public Guid TransportNeedId { get; set; }
    public virtual TransportNeed? TransportNeed { get; set; }

    public Guid TransportOfferId { get; set; }
    public virtual TransportOffer? TransportOffer { get; set; }
    public ETransportStatus Status { get; set; }

    public ICollection<Schedule>? Schedules { get; set; }
}