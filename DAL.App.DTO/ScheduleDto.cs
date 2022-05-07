
using DAL.App.DTO.Common;

namespace DAL.App.DTO;

public class ScheduleDto : BaseDto
{
    public Guid? TransportOfferId { get; set; }
    public virtual TransportOfferDto? TransportOffer { get; set; }

    public Guid? TransportNeedId { get; set; }
    public virtual TransportNeedDto? TransportNeed { get; set; }

    public Guid? TransportId { get; set; }
    public virtual TransportDto? Transport { get; set; }

    public string Name { get; set; } = default!;
}