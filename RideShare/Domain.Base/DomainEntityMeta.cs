using Contracts.Domain.Base;

namespace Domain.Base
{
    public class DomainEntityMeta : IDomainEntityMeta
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}