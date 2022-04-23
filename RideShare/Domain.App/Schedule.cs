using Domain.Base;

namespace Domain.App;

public class Schedule : DomainEntity
{
    public Guid? TransportOfferId { get; set; }
    public virtual TransportOffer? TransportOffer { get; set; }

    public Guid? TransportNeedId { get; set; }
    public virtual TransportNeed? TransportNeed { get; set; }

    public Guid? TransportId { get; set; }
    public virtual Transport? Transport { get; set; }

    public DateTime ScheduledAt { get; set; }
}