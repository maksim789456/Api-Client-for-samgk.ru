using ClientSamgk.Models;
using ClientSamgk.Models.Api.Interfaces.Cabs;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;
using ClientSamgk.Models.Api.Interfaces.Schedule;
using ClientSamgk.Models.Enums.Schedule;

namespace ClientSamgk.Common;

public class CommonCache
{
    protected int DefaultLifeTimeInMinutesForCommon = 2880; // 2 дня
    protected int DefaultLifeTimeInMinutesLong = 43200; // 1 месяц
    protected int DefaultLifeTimeInMinutesShort = 10; // 10минут

    protected readonly Cache<IResultOutScheduleFromDate> ScheduleCache = new();
    protected readonly Cache<IResultOutCab> CabsCache = new();
    protected readonly Cache<IResultOutGroup> GroupsCache = new();
    protected readonly Cache<IResultOutIdentity> IdentityCache = new();

    protected bool ForceUpdateCache =>
        CabsCache.IsCacheOutdated() || GroupsCache.IsCacheOutdated() || IdentityCache.IsCacheOutdated();

    protected IResultOutScheduleFromDate? ExtractFromCache(DateOnly date, ScheduleSearchType type, string? id) =>
        ScheduleCache.ExtractFromCache(x => x.Date == date && x.SearchType == type && x.IdValue == id);

    protected IResultOutCab? ExtractCabFromCache(string? id) => CabsCache.ExtractFromCache(x => x.Adress == id);
    protected IResultOutGroup? ExtractGroupFromCache(long? id) => GroupsCache.ExtractFromCache(x => x.Id == id);
    protected IResultOutIdentity? ExtractIdentityFromCache(long? id) => IdentityCache.ExtractFromCache(x => x.Id == id);
}