using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base.Identity;

public class BaseRole<TUserRole> : BaseRole<Guid, TUserRole>, IDomainEntity
    where TUserRole : IdentityUserRole<Guid>
{
    public BaseRole() : base()
    {
    }
    public BaseRole(string roleName) : base(roleName)
    {
    }
}

public class BaseRole<TKey, TUserRole> : IdentityRole<TKey>, IDomainEntity<TKey>
    where TKey : IEquatable<TKey>
    where TUserRole : IdentityUserRole<TKey>
{
    public BaseRole() : base()
    {
    }
    public BaseRole(string roleName) : base(roleName)
    {
    }


    public string DisplayName { get; set; } = default!;
    public virtual ICollection<TUserRole>? UserRoles { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}