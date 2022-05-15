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
    private readonly TransportOfferMapper _transportOfferMapper;
    public TransportOfferService(IAppUnitOfWork serviceUow, ITransportOfferRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportOfferMapper(mapper))
    {
        _transportOfferMapper = new TransportOfferMapper(mapper);
    }

    public async Task<IEnumerable<DALAppDTO.TransportOfferDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true)
    {
        return await ServiceRepository.GetAllWithIncludingsAsync(userId, noTracking);
    }

    public Task<IEnumerable<DALAppDTO.TransportOfferDto>> GetByCountAsync(int count)
    {
        throw new NotImplementedException();
    }
}