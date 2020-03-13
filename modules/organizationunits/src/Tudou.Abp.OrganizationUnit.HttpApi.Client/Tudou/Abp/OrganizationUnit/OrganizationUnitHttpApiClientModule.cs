using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Tudou.Abp.OrganizationUnit
{
    [DependsOn(
        typeof(OrganizationUnitApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class OrganizationUnitHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "OrganizationUnit";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(OrganizationUnitApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
