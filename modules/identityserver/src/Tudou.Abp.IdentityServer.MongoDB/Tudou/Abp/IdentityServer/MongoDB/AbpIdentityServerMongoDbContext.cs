using MongoDB.Driver;
using Volo.Abp.Data;
using Tudou.Abp.IdentityServer.ApiResources;
using Tudou.Abp.IdentityServer.Clients;
using Tudou.Abp.IdentityServer.Devices;
using Tudou.Abp.IdentityServer.Grants;
using Volo.Abp.MongoDB;
using IdentityResource = Tudou.Abp.IdentityServer.IdentityResources.IdentityResource;

namespace Tudou.Abp.IdentityServer.MongoDB
{
    [ConnectionStringName(AbpIdentityServerDbProperties.ConnectionStringName)]
    public class AbpIdentityServerMongoDbContext : AbpMongoDbContext, IAbpIdentityServerMongoDbContext
    {
        public IMongoCollection<ApiResource> ApiResources => Collection<ApiResource>();

        public IMongoCollection<Client> Clients => Collection<Client>();

        public IMongoCollection<IdentityResource> IdentityResources => Collection<IdentityResource>();

        public IMongoCollection<PersistedGrant> PersistedGrants => Collection<PersistedGrant>();

        public IMongoCollection<DeviceFlowCodes> DeviceFlowCodes => Collection<DeviceFlowCodes>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureIdentityServer();
        }
    }
}
