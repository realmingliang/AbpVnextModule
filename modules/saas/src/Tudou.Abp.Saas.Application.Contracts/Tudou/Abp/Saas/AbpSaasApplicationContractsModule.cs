using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Tudou.Abp.Saas
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpSaasDomainSharedModule))]
    public class AbpSaasApplicationContractsModule : AbpModule
    {

    }
}