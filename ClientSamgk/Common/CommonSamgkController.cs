using ClientSamgk.Models;
using ClientSamgk.Models.Api.Implementation.Cabs;
using ClientSamgk.Models.Api.Implementation.Groups;
using ClientSamgk.Models.Api.Implementation.Identity;
using ClientSamgk.Models.Api.Interfaces.Cabs;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;
using ClientSamgk.Models.Api.Mfc.Groups;
using ClientSamgk.Models.Api.Mfc.Teachers;
using ClientSamgk.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace ClientSamgk.Common;

public class CommonSamgkController(RestClient restClient) : CommonCache
{
    protected async Task UpdateIfCacheIsOutdated(CancellationToken cToken = default)
    {
        if (!IsRequiredToForceUpdateCache()) return;

        await ConfiguringCacheTeachers(cToken).ConfigureAwait(false);
        await ConfiguringCacheCabs(cToken).ConfigureAwait(false);
        await ConfiguringCacheGroups(cToken).ConfigureAwait(false);
    }

    private async Task ConfiguringCacheGroups(CancellationToken cToken = default)
    {
        var resultApiGroups = await restClient
            .SendRequest<IList<SamGkGroupApiResult>>(new Uri("https://mfc.samgk.ru/api/groups"), cToken: cToken)
            .ConfigureAwait(false);

        if (resultApiGroups == null || !resultApiGroups.Any()) return;

        GroupsCache = new List<LifeTimeMemory<IResultOutGroup>>();

        var items = resultApiGroups
            .Select(IResultOutGroup (x) => new ResultOutGroup
            {
                Id = x.Id,
                Name = x.Name,
                Currator = ExtractIdentityFromCache(x.Currator),
            })
            .OrderBy(x => x.Name)
            .Where(x => x.Course <= 5)
            .ToList();

        foreach (var item in items) SaveToCache(item, DefaultLifeTimeInMinutesForCommon);
    }

    private async Task ConfiguringCacheTeachers(CancellationToken cToken = default)
    {
        var resultApiTeachers = await restClient
            .SendRequest<IList<SamgkTeacherApiResult>>(new Uri("https://mfc.samgk.ru/api/teachers"), cToken: cToken)
            .ConfigureAwait(false);

        if (resultApiTeachers == null || !resultApiTeachers.Any()) return;

        IdentityCache = new List<LifeTimeMemory<IResultOutIdentity>>();

        var items = resultApiTeachers
            .Select(IResultOutIdentity (x) => new ResultOutIdentity
            {
                Id = Convert.ToInt64(x.Id),
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToList();

        foreach (var item in items) SaveToCache(item, DefaultLifeTimeInMinutesForCommon);
    }

    private async Task ConfiguringCacheCabs(CancellationToken cToken = default)
    {
        var resultApiCabs = await restClient
            .SendRequest<Dictionary<string, string>>(new Uri("https://mfc.samgk.ru/api/cabs"), cToken: cToken)
            .ConfigureAwait(false);

        if (resultApiCabs == null || !resultApiCabs.Any()) return;

        CabsCache = new List<LifeTimeMemory<IResultOutCab>>();

        var items = resultApiCabs
            .Select(IResultOutCab (x) => new ResultOutCab
            {
                Adress = x.Value
            })
            .OrderBy(x => x.Adress)
            .ToList();

        foreach (var item in items) SaveToCache(item, DefaultLifeTimeInMinutesForCommon);
    }
}