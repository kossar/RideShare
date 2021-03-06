using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class QuestionAnswerRepository : BaseRepository<QuestionAnswerDto, QuestionAnswer, AppDbContext>, IQuestionAnswerRepository
{
    public QuestionAnswerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuestionAnswerMapper(mapper))
    {
    }
}