using Volo.Abp.Modularity;

namespace Tudou.Grace
{
    [DependsOn(
        typeof(GraceApplicationModule),
        typeof(GraceDomainTestModule)
        )]
    public class GraceApplicationTestModule : AbpModule
    {

    }
}