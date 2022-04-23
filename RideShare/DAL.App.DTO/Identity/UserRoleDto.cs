using DAL.App.DTO.Common;

namespace DAL.App.DTO.Identity;

public class UserRoleDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public Guid RoleId { get; set; }
    public virtual RoleDto? Role { get; set; }
}