using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class QuestionAnswer : DomainEntity, IDomainUserId, IDomainUser<User>
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
}