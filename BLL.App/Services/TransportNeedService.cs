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
    public TransportNeedService(IAppUnitOfWork serviceUow, ITransportNeedRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransportNeedMapper(mapper))
    {
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
}