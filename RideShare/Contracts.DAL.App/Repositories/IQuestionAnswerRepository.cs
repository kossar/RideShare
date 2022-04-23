using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface IQuestionAnswerRepository : IBaseRepository<QuestionAnswerDto>, IQuestionAnswerRepositoryCustom<QuestionAnswerDto>
{
    
}

public interface IQuestionAnswerRepositoryCustom<TEntity>
{

}