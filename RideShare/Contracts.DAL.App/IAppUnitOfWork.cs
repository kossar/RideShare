using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App;

public interface IAppUnitOfWork : IBaseUnitOfWork
{
    ITransportRepository Transports { get; }
    ITransportOfferRepository TransportOffers { get; }
    ITransportNeedRepository TransportNeeds { get; }
    IVehicleRepository Vehicles { get; }
    ILocationRepository Locations { get; }
    IScheduleRepository Schedules { get; }
    IQuestionAnswerRepository QuestionAnswers { get; }
}