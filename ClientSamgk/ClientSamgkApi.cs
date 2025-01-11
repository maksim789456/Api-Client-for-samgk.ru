using ClientSamgk.Common;
using ClientSamgk.Controllers;
using ClientSamgk.Interfaces.Client;
using ClientSamgk.Models.Params.Interfaces.Cache;
using RestSharp;

namespace ClientSamgk;

public class ClientSamgkApi : IClientSamgkApi
{
    private readonly RestClient _restClient;

    public ISсheduleController Schedule { get; protected set; }
    public IIdentityController Accounts { get; protected set; }
    public IGroupController Groups { get; protected set; }
    public ICabController Cabs { get; protected set; }
    public IMemoryCacheController Cache { get; protected set; }

    public ClientSamgkApi()
    {
        _restClient = new RestClient(new HttpClient());

        Schedule = new ScheduleController(_restClient);
        Accounts = new AccountController(_restClient);
        Groups = new GroupsController(_restClient);
        Cabs = new CabsController(_restClient);
        Cache = new MemoryCacheController(_restClient);
    }

    public ClientSamgkApi(ICacheOptions cacheOptions) : this()
    {
        Cache.SetLifeTime(cacheOptions);
    }
}