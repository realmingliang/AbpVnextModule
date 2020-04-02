using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.IdentityServer.Devices;
using Tudou.Abp.IdentityServer.Grants;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using ApiResource = Tudou.Abp.IdentityServer.ApiResources.ApiResource;
using Client = Tudou.Abp.IdentityServer.Clients.Client;
using IdentityResource = Tudou.Abp.IdentityServer.IdentityResources.IdentityResource;

namespace Tudou.Abp.IdentityServer.MongoDB
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpMongoDbModule)
    )]
    public class AbpIdentityServerMongoDbModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IIdentityServerBuilder>(
                builder =>
                {
                    builder.AddAbpStores();
                }
            );
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<AbpIdentityServerMongoDbContext>(options =>
            {
                options.AddRepository<ApiResource, MongoApiResourceRepository>();
                options.AddRepository<IdentityResource, MongoIdentityResourceRepository>();
                options.AddRepository<Client, MongoClientRepository>();
                options.AddRepository<PersistedGrant, MongoPersistentGrantRepository>();
                options.AddRepository<DeviceFlowCodes, MongoDeviceFlowCodesRepository>();
            });
        }
    }
}
