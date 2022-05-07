using BLL.App.DTO.Common;
using BLL.App.DTO.Enums;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO;

public class TransportDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public Guid TransportNeedId { get; set; }
    public virtual TransportNeedDto? TransportNeed{ get; set; }
    public Guid TransportOfferId { get; set; }
    public virtual TransportOfferDto? TransportOffer { get; set; }
    public ETransportStatus Status { get; set; }

    public ICollection<ScheduleDto>? Schedules { get; set; }
}