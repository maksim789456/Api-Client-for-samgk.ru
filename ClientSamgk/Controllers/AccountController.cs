using ClientSamgk.Common;
using ClientSamgk.Interfaces.Client;
using ClientSamgk.Models.Api.Interfaces.Identity;
using RestSharp;

namespace ClientSamgk.Controllers;

public class AccountController(RestClient client) : CommonSamgkController(client), IIdentityController
{
    public IList<IResultOutIdentity> GetTeachers()
    {
        return GetTeachersAsync().GetAwaiter().GetResult();
    }

    public async Task<IList<IResultOutIdentity>> GetTeachersAsync()
    {
        await UpdateIfCacheIsOutdated().ConfigureAwait(false);
        return IdentityCache.Data.Select(x => x.Object).OrderBy(x => x.Name).ToList();
    }

    public IResultOutIdentity? GetTeacher(string teacherName)
    {
        return GetTeacherAsync(teacherName).GetAwaiter().GetResult();
    }

    public IResultOutIdentity? GetTeacher(long id)
    {
        return GetTeacherAsync(id).GetAwaiter().GetResult();
    }

    public async Task<IResultOutIdentity?> GetTeacherAsync(long id)
    {
        await UpdateIfCacheIsOutdated().ConfigureAwait(false);
        return IdentityCache.ExtractFromCache(x => x.Id == id);
    }

    public async Task<IResultOutIdentity?> GetTeacherAsync(string teacherName)
    {
        await UpdateIfCacheIsOutdated().ConfigureAwait(false);
        return IdentityCache.Data.Select(x => x.Object)
            .FirstOrDefault(x => string.Equals(x.Name, teacherName, StringComparison.CurrentCultureIgnoreCase));
    }
}