using API.DTO.v1.Models.QuestionAnswer;
using AutoMapper;

namespace API.DTO.v1.Mappers;

public class QuestionAnswerMapper : BaseMapper<QuestionAnswerModel, BLL.App.DTO.QuestionAnswerDto>
{
    private readonly IMapper _mapper;
    public QuestionAnswerMapper(IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
    }

    public BLL.App.DTO.QuestionAnswerDto MapToBll(QuestionAnswerModel QuestionAnswerAdd)
    {
        var bllQuestionAnswer = _mapper.Map<BLL.App.DTO.QuestionAnswerDto>(QuestionAnswerAdd);
        return bllQuestionAnswer;
    }
}