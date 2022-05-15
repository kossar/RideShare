using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories;

public interface ITransportNeedRepository : IBaseRepository<TransportNeedDto>, ITransportNeedRepositoryCustom<TransportNeedDto>
{
    
}

public interface ITransportNeedRepositoryCustom<TEntity>
{
    Task<IEnumerable<TransportNeedDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true);
    Task<IEnumerable<TransportNeedDto>> GetByCountAsync(int count);
}