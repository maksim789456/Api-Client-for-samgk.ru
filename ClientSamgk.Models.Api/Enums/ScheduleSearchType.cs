using System.ComponentModel.DataAnnotations;

namespace ClientSamgk.Models.Api.Enums;

public enum ScheduleSearchType
{
    [Display(Name = "Преподаватель")]
    Employee, 
    [Display(Name = "Группа")]
    Group, 
    [Display(Name = "Кабинет")]
    Cab
}