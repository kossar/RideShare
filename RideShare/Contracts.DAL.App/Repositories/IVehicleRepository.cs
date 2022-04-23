using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories;

public interface IVehicleRepository : IBaseRepository<VehicleDto>, IVehicleRepositoryCustom<VehicleDto>
{
    
}

public interface IVehicleRepositoryCustom<TEntity>
{

}