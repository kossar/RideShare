using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface IScheduleRepository : IBaseRepository<ScheduleDto>, IScheduleRepositoryCustom<ScheduleDto>
{
    
}

public interface IScheduleRepositoryCustom<TEntity>
{

}