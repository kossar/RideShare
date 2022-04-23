using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Vehicle : DomainEntity, IDomainUserId, IDomainUser<User>
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string Number { get; set; } = default!;
    public ICollection<TransportOffer>? TransportOffers { get; set; }
}