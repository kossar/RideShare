using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface ITransportOfferRepository : IBaseRepository<TransportOfferDto>, ITransportOfferRepositoryCustom<TransportOfferDto>
{
    
}

public interface ITransportOfferRepositoryCustom<TEntity>
{

}