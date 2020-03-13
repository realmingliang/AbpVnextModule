using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Tudou.Abp.OrganizationUnit
{
    [DependsOn(
        typeof(OrganizationUnitDomainSharedModule),
           typeof(AbpIdentityDomainModule)
        )]
    public class OrganizationUnitDomainModule : AbpModule
    {

    }
}
