using BLL.App.DTO.Common;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO;

public class QuestionAnswerDto : BaseDto
{
    public Guid UserId { get; set; }
    public virtual UserDto? User { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
}