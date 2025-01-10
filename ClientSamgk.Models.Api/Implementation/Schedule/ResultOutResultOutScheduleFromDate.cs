using ClientSamgk.Models.Api.Enums;
using ClientSamgk.Models.Api.Interfaces.Schedule;

namespace ClientSamgk.Models.Api.Implementation.Schedule;

public class ResultOutResultOutScheduleFromDate : IResultOutScheduleFromDate
{
    public DateOnly Date { get; set; }
    public IList<IResultOutLesson> Lessons { get; set; } = new List<IResultOutLesson>();
    public ScheduleSearchType SearchType { get; set; }
    public string IdValue { get; set; } = string.Empty;
    public ScheduleCallType CallType { get; set; }
}