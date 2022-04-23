using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;

        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public ITransportService Transports =>
            GetService<ITransportService>(() => new TransportService(Uow, Uow.Transports, Mapper));

        public ITransportOfferService TransportOffers =>
            GetService<ITransportOfferService>(() => new TransportOfferService(Uow, Uow.TransportOffers, Mapper));

        public ITransportNeedService TransportNeeds =>
            GetService<ITransportNeedService>(() => new TransportNeedService(Uow, Uow.TransportNeeds, Mapper));

        public IVehicleService Vehicles =>
            GetService<IVehicleService>(() => new VehicleService(Uow, Uow.Vehicles, Mapper));

        public ILocationService Locations =>
            GetService<ILocationService>(() => new LocationService(Uow, Uow.Locations, Mapper));

        public IScheduleService Schedules =>
            GetService<IScheduleService>(() => new ScheduleService(Uow, Uow.Schedules, Mapper));

        public IQuestionAnswerService QuestionAnswers=>
            GetService<IQuestionAnswerService>(() => new QuestionAnswerService(Uow, Uow.QuestionAnswers, Mapper));

    }
}