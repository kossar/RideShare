using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base.Identity;

public class BaseUser<TUserRole> : BaseUser<Guid, TUserRole>, IDomainEntity
    where TUserRole : IdentityUserRole<Guid>
{
}

public class BaseUser<TKey, TUserRole> : IdentityUser<TKey>, IDomainEntity<TKey>
    where TKey : IEquatable<TKey>
    where TUserRole : IdentityUserRole<TKey>
{
    public virtual string FirstName { get; set; } = default!;

    public virtual string LastName { get; set; } = default!;


    public virtual ICollection<TUserRole>? UserRoles { get; set; }
    public virtual string FullName => FirstName + " " + LastName;
    public virtual string FullNameEmail => FullName + " (" + Email + ")";
    public virtual string FirstLastName => FirstName + " " + LastName;
    public virtual string LastFirstName => LastName + " " + FirstName;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}