using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services;

public interface IVehicleService : IBaseEntityService<BLLAppDTO.VehicleDto, VehicleDto>, IVehicleRepositoryCustom<VehicleDto>
{

}