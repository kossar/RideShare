using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF;

public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    private readonly IMapper _mapper;
    public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
    {
        _mapper = mapper;
    }

    public ITransportOfferRepository TransportOffers =>
        GetRepository(() => new TransportOfferRepository(UowDbContext, _mapper));

    public ITransportNeedRepository TransportNeeds => 
         GetRepository(() => new TransportNeedRepository(UowDbContext, _mapper));

    public ITransportRepository Transports =>
        GetRepository(() => new TransportRepository(UowDbContext, _mapper));

    public IVehicleRepository Vehicles => 
        GetRepository(() => new VehicleRepository(UowDbContext, _mapper));

    public ILocationRepository Locations => 
        GetRepository(() => new LocationRepository(UowDbContext, _mapper));

    public IScheduleRepository Schedules => 
        GetRepository(() => new ScheduleRepository(UowDbContext, _mapper));

    public IQuestionAnswerRepository QuestionAnswers =>
        GetRepository(() => new QuestionAnswerRepository(UowDbContext, _mapper));
}