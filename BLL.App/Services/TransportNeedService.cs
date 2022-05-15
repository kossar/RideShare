using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services;

public class TransportNeedService : BaseEntityService<IAppUnitOfWork, ITransportNeedRepository, BLLAppDTO.TransportNeedDto, DALAppDTO.TransportNeedDto>, ITransportNeedService
{
    private readonly TransportNeedMapper _transportNeedMapper;
    public TransportNeedService(IAppUnitOfWork serviceUow, ITransportNeedRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportNeedMapper(mapper))
    {
        _transportNeedMapper = new TransportNeedMapper(mapper);
    }

    //public async Task<BLL.App.DTO.TransportNeedDto> AddWithScheduleAsync(BLL.App.DTO.TransportNeedDto transportNeed)
    //{
    //    var dalTransportNeed = Mapper.Map(transportNeed);
    //    var transportNeeds = new List<DALAppDTO.TransportNeedDto>();
    //    foreach (var schedule in transportNeed.Schedules!)
    //    {
    //        transportNeeds.Add(dalTransportNeed!);
    //    }
        
    //}
    public async Task<IEnumerable<DALAppDTO.TransportNeedDto>> GetAllWithIncludingsAsync(Guid userId = default, bool noTracking = true)
    {
        return await ServiceRepository.GetAllWithIncludingsAsync(userId, noTracking);
    }

    public Task<IEnumerable<DALAppDTO.TransportNeedDto>> GetByCountAsync(int count)
    {
        throw new NotImplementedException();
    }
}