using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Tudou.Abp.Saas
{
    [DependsOn(
        typeof(AbpSaasApplicationContractsModule), 
        typeof(AbpHttpClientModule))]
    public class AbpSaasHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpSaasApplicationContractsModule).Assembly,
                SaasRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}