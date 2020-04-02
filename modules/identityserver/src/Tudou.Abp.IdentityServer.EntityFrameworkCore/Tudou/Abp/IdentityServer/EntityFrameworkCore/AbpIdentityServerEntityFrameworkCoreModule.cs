using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Tudou.Abp.IdentityServer.ApiResources;
using Tudou.Abp.IdentityServer.Clients;
using Tudou.Abp.IdentityServer.Devices;
using Tudou.Abp.IdentityServer.Grants;
using Tudou.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Modularity;

namespace Tudou.Abp.IdentityServer.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
        )]
    public class AbpIdentityServerEntityFrameworkCoreModule : AbpModule
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
            context.Services.AddAbpDbContext<IdentityServerDbContext>(options =>
            {
                options.AddDefaultRepositories<IIdentityServerDbContext>();

                options.AddRepository<Client, ClientRepository>();
                options.AddRepository<ApiResource, ApiResourceRepository>();
                options.AddRepository<IdentityResource, IdentityResourceRepository>();
                options.AddRepository<PersistedGrant, PersistentGrantRepository>();
                options.AddRepository<DeviceFlowCodes, DeviceFlowCodesRepository>();
            });
        }
    }
}
