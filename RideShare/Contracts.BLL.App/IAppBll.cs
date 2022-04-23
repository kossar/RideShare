using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App;

public interface IAppBLL : IBaseBLL
{
    ITransportService Transports { get; }
    ITransportOfferService TransportOffers { get; }
    ITransportNeedService TransportNeeds { get; }
    IScheduleService Schedules { get; }
    ILocationService Locations { get; }
    IVehicleService Vehicles { get; }
    IQuestionAnswerService QuestionAnswers { get; }
}