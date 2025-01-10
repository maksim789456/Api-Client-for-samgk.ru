using ClientSamgk.Models.Api.Implementation.Schedule;
using ClientSamgk.Models.Api.Interfaces.Cabs;
using ClientSamgk.Models.Api.Interfaces.Education;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;

namespace ClientSamgk.Models.Api.Interfaces.Schedule;

public interface IResultOutLesson
{
    /// <summary>
    /// Список преподавателей ведущий урок
    /// </summary>
    public IList<IResultOutIdentity> Identity { get; set; }
    /// <summary>
    /// Информация о группеы
    /// </summary>
    public IResultOutGroup? EducationGroup { get; set; }
    /// <summary>
    /// Информация о предмете
    /// </summary>
    public IResultOutSubjectItem SubjectDetails { get; set; }
    /// <summary>
    /// Список кабинетов в которых проходят занятия
    /// </summary>
    public IList<IResultOutCab> Cabs { get; set; }
    /// <summary>
    /// Номер пары
    /// </summary>
    public long NumPair { get; set; }
    /// <summary>
    /// Номер урока
    /// </summary>
    public long NumLesson { get; set; }
    /// <summary>
    /// Информация о длительности занятий
    /// </summary>
    public IList<DurationLessonDetails> Durations { get; set; }
}