using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface ITransportOfferRepository : IBaseRepository<TransportOfferDto>, ITransportOfferRepositoryCustom<TransportOfferDto>
{
    
}

public interface ITransportOfferRepositoryCustom<TEntity>
{
    Task<IEnumerable<TransportOfferDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true);
    Task<IEnumerable<TransportOfferDto>> GetByCountAsync(int count);
}