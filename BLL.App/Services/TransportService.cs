using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class TransportService : BaseEntityService<IAppUnitOfWork, ITransportRepository, BLLAppDTO.TransportDto, DALAppDTO.TransportDto>, ITransportService
{
    public TransportService(IAppUnitOfWork serviceUow, ITransportRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportMapper(mapper))
    {
    }

}