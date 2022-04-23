using AutoMapper;
using DAL.App.DTO;
using Domain.App;

namespace DAL.App.EF.Mappers;

public class QuestionAnswerMapper : BaseMapper<QuestionAnswerDto, QuestionAnswer>
{
    public QuestionAnswerMapper(IMapper mapper) : base(mapper)
    {
    }
}