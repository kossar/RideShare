using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Location : DomainEntity, IDomainUserId, IDomainUser<User>
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public string Country { get; set; } = default!;
        public string? Province { get; set; }
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<TransportOffer>? TransportOfferStartLocations { get; set; }
        public ICollection<TransportOffer>? TransportOfferDestinationLocations { get; set; }
        public ICollection<TransportNeed>? TransportNeedStartLocations { get; set; }
        public ICollection<TransportNeed>? TransportNeedDestinationLocations { get; set; }
    }
}