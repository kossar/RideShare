using Domain.Base;

namespace Domain.App;

public class Schedule : DomainEntity
{
   public string Name { get; set; } = default!;
    public virtual ICollection<TransportNeed>? TransportNeeds{ get; set; }
    public virtual ICollection<TransportOffer>? TransportOffers{ get; set; }
}