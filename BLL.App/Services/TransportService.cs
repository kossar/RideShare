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
    private readonly TransportMapper _transportMapper;
    public TransportService(IAppUnitOfWork serviceUow, ITransportRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportMapper(mapper))
    {
        _transportMapper = new TransportMapper(mapper);
    }


    public async Task<IEnumerable<BLLAppDTO.TransportAdListDto>> GetLastOffersAndNeedsByCount(int count, Guid? userId)
    {
        var transportNeeds = await ServiceUow.TransportNeeds.GetByCountAsync(count);
        var transportOffers = await ServiceUow.TransportOffers.GetByCountAsync(count);

        var needs = _transportMapper.TransportNeedsToTransportListItem(transportNeeds, userId);

        var offers = _transportMapper.TransportOffersToTransportListItem(transportOffers, userId);

        return needs.Concat(offers).OrderByDescending(ad => ad.CreatedAt).Take(count);
    }

}