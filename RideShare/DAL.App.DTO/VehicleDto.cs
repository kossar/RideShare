using DAL.App.DTO.Common;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class VehicleDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string Number { get; set; } = default!;
    public ICollection<TransportDto>? Transports { get; set; }
}