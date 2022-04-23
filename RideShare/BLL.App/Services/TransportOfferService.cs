using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class TransportOfferService : BaseEntityService<IAppUnitOfWork, ITransportOfferRepository, BLLAppDTO.TransportOfferDto, DALAppDTO.TransportOfferDto>, ITransportOfferService
{
    public TransportOfferService(IAppUnitOfWork serviceUow, ITransportOfferRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportOfferMapper(mapper))
    {
    }

}