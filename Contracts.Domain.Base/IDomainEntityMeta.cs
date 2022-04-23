namespace Contracts.Domain.Base;

public interface IDomainEntityMeta
{
    public bool IsDeleted { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
    DateTime? DeletedAt { get; set; }
}