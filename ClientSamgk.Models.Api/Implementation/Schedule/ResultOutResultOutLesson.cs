using ClientSamgk.Models.Api.Implementation.Education;
using ClientSamgk.Models.Api.Implementation.Groups;
using ClientSamgk.Models.Api.Interfaces.Cabs;
using ClientSamgk.Models.Api.Interfaces.Education;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;
using ClientSamgk.Models.Api.Interfaces.Schedule;

namespace ClientSamgk.Models.Api.Implementation.Schedule;

public class ResultOutResultOutLesson : IResultOutLesson
{
    public IList<IResultOutIdentity> Identity { get; set; } = new List<IResultOutIdentity>();
    public IResultOutGroup? EducationGroup { get; set; } = new ResultOutGroup();
    public IResultOutSubjectItem SubjectDetails { get; set; } = new ResultOutSubject();
    public IList<IResultOutCab> Cabs { get; set; } = new List<IResultOutCab>();
    public long NumPair { get; set; }
    public long NumLesson { get; set; }
    public IList<DurationLessonDetails> Durations { get; set; } = new List<DurationLessonDetails>();

}