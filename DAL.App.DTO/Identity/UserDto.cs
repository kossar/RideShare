
namespace DAL.App.DTO.Identity;

public class UserDto
{
    public Guid Id { get; set; }

    public ICollection<TransportNeedDto>? TransportNeeds { get; set; }
    public ICollection<TransportOfferDto>? TransportOffers { get; set; }
    public ICollection<TransportDto>? Transports { get; set; }
    public ICollection<VehicleDto>? Vehicles { get; set; }
    public ICollection<QuestionAnswerDto>? QuestionAnswers { get; set; }
}