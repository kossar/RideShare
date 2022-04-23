using BLL.App.DTO.Common;

namespace BLL.App.DTO.Identity;

public class RoleDto : BaseDto
{
    public string DisplayName { get; set; } = default!;
    public ICollection<UserRole>? UserRoles { get; set; }
}