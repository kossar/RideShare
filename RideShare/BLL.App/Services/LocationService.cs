using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class LocationService : BaseEntityService<IAppUnitOfWork, ILocationRepository, BLLAppDTO.LocationDto, DALAppDTO.LocationDto>, ILocationService
{
    public LocationService(IAppUnitOfWork serviceUow, ILocationRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LocationMapper(mapper))
    {
    }

}