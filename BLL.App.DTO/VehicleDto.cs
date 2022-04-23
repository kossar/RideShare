using BLL.App.DTO.Common;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO;

public class VehicleDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string Number { get; set; } = default!;
    public ICollection<TransportDto>? Transports { get; set; }
}