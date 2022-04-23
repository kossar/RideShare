using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface ILocationRepository : IBaseRepository<LocationDto>, ILocationRepositoryCustom<LocationDto>
{
    
}

public interface ILocationRepositoryCustom<TEntity>
{

}