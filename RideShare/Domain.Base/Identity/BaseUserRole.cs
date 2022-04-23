using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base.Identity;

public class BaseUserRole<TUser, TRole> : BaseUserRole<Guid, TUser, TRole>, IDomainEntityMeta
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
}

public class BaseUserRole<TKey, TUser, TRole> : IdentityUserRole<TKey>, IDomainEntityMeta
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
{
    public virtual TUser? User { get; set; }
    public virtual TRole? Role { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}