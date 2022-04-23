using Domain.Base.Identity;

namespace Domain.App.Identity;

public class User : BaseUser<UserRole>
{
    public ICollection<TransportNeed>? TransportNeeds { get; set; }
    public ICollection<TransportOffer>? TransportOffers { get; set; }
    public ICollection<Transport>? Transports { get; set; }
    public ICollection<Vehicle>? Vehicles { get; set; }
    public ICollection<Location>? Locations { get; set; }
    public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
}