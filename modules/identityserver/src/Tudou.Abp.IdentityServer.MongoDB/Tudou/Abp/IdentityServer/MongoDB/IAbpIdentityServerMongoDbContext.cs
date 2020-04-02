using MongoDB.Driver;
using Volo.Abp.Data;
using Tudou.Abp.IdentityServer.Clients;
using Tudou.Abp.IdentityServer.Devices;
using Tudou.Abp.IdentityServer.Grants;
using Tudou.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MongoDB;
using ApiResource = Tudou.Abp.IdentityServer.ApiResources.ApiResource;

namespace Tudou.Abp.IdentityServer.MongoDB
{
    [ConnectionStringName(AbpIdentityServerDbProperties.ConnectionStringName)]
    public interface IAbpIdentityServerMongoDbContext : IAbpMongoDbContext
    {
        IMongoCollection<ApiResource> ApiResources { get; }

        IMongoCollection<Client> Clients { get; }

        IMongoCollection<IdentityResource> IdentityResources { get; }

        IMongoCollection<PersistedGrant> PersistedGrants { get; }

        IMongoCollection<DeviceFlowCodes> DeviceFlowCodes { get; }
    }
}
