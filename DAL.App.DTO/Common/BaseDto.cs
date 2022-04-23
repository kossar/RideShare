using Contracts.Domain.Base;

namespace DAL.App.DTO.Common;

public class BaseDto : IDomainEntityId
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}