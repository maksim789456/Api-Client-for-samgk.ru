using ClientSamgk.Common;
using ClientSamgk.Interfaces.Client;
using ClientSamgk.Models;
using ClientSamgk.Models.Api.Interfaces.Cabs;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;
using ClientSamgk.Models.Api.Interfaces.Schedule;
using ClientSamgk.Models.Params.Interfaces.Cache;
using RestSharp;

namespace ClientSamgk.Controllers;

public class MemoryCacheController(RestClient client) : CommonSamgkController(client), IMemoryCacheController
{
    public async Task ClearIfOutDateAsync()
    {
        await UpdateIfCacheIsOutdated().ConfigureAwait(false);
        CabsCache.ClearCache();
        IdentityCache.ClearCache();
        GroupsCache.ClearCache();
    }

    public void ClearIfOutDate()
    {
        ClearIfOutDateAsync().GetAwaiter().GetResult();
    }

    public void Clear()
    {
        CabsCache.DropCache();
        IdentityCache.DropCache();
        GroupsCache.DropCache();
        ScheduleCache.DropCache();
    }

    public void SetLifeTime(ICacheOptions options)
    {
        DefaultLifeTimeInMinutesForCommon = options.LifeTimeCommonObjectsObjects;
        DefaultLifeTimeInMinutesLong = options.LifeTimeObjectsForLong;
        DefaultLifeTimeInMinutesShort = options.LifeTimeObjectsForShort;
    }
}