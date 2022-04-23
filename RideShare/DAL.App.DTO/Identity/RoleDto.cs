
using DAL.App.DTO.Common;

namespace DAL.App.DTO.Identity;

public class RoleDto : BaseDto
{
    public string DisplayName { get; set; } = default!;
    public ICollection<UserRoleDto>? UserRoles { get; set; }
}