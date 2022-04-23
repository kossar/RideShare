using Contracts.Domain.Base;

namespace Domain.Base;

public class DomainEntity : DomainEntity<Guid>, IDomainEntity, IDomainEntityId
{
}

public class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}