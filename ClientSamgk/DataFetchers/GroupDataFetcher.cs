﻿using ClientSamgk.Cache;
using ClientSamgk.Interfaces;
using ClientSamgk.Models.Api.Implementation.Groups;
using ClientSamgk.Models.Api.Interfaces.Groups;
using ClientSamgk.Models.Api.Interfaces.Identity;
using ClientSamgk.Models.Api.Mfc.Groups;
using ClientSamgk.Utils;
using RestSharp;

namespace ClientSamgk.DataFetchers;

public class GroupDataFetcher(ICache<IResultOutIdentity> teacherCache, RestClient client)
    : IDataFetcher<IResultOutGroup>
{
    public async Task<IEnumerable<IResultOutGroup>> FetchAsync(CancellationToken cToken = default)
    {
        var resultApiGroups = await client
            .SendRequest<IList<SamGkGroupApiResult>>(new Uri("https://mfc.samgk.ru/api/groups"), cToken: cToken)
            .ConfigureAwait(false);

        if (resultApiGroups == null || !resultApiGroups.Any())
            return Array.Empty<IResultOutGroup>();

        return resultApiGroups
            .Select(IResultOutGroup (x) => new ResultOutGroup
            {
                Id = x.Id,
                Name = x.Name,
                Currator = teacherCache.ExtractFromCache(o => o.Id == x.Currator)
            })
            .OrderBy(x => x.Name)
            .Where(x => x.Course <= 5);
    }
}