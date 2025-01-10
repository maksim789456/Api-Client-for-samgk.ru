namespace ClientSamgk.Models.Api.Implementation.Schedule;

public class DurationLessonDetails
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public DurationLessonDetails(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public DurationLessonDetails()
    {
            
    }
}