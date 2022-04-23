

using DAL.App.DTO.Common;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class QuestionAnswerDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
}