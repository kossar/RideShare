using AutoMapper;

namespace BLL.App.Mappers;

public class QuestionAnswerMapper : BaseMapper<BLL.App.DTO.QuestionAnswerDto, DAL.App.DTO.QuestionAnswerDto>
{
    public QuestionAnswerMapper(IMapper mapper) : base(mapper)
    {
    }
}