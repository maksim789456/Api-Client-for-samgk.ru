using ClientSamgk.Models;
using ClientSamgk.Models.Api.Interfaces.Schedule;

namespace ClientSamgk.Interfaces.Client;

public interface ISсheduleController
{
    public IList<IResultOutScheduleFromDate> GetSchedule(ScheduleQuery query);

    public Task<IList<IResultOutScheduleFromDate>> GetScheduleAsync(ScheduleQuery query, CancellationToken cToken = default);
}