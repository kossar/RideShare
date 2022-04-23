using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface ITransportRepository : IBaseRepository<TransportDto>, ITransportRepositoryCustom<TransportDto>
{
    
}

public interface ITransportRepositoryCustom<TEntity>
{

}