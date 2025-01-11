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

    protected readonly CacheStore<IResultOutScheduleFromDate> ScheduleCache = new();
    protected readonly CacheStore<IResultOutCab> CabsCache = new();
    protected readonly CacheStore<IResultOutGroup> GroupsCache = new();
    protected readonly CacheStore<IResultOutIdentity> IdentityCache = new();

    protected bool ForceUpdateCache =>
        CabsCache.IsCacheOutdated() || GroupsCache.IsCacheOutdated() || IdentityCache.IsCacheOutdated();
}