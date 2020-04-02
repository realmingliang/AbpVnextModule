using Tudou.Grace.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tudou.Grace
{
    [DependsOn(
        typeof(GraceEntityFrameworkCoreTestModule)
        )]
    public class GraceDomainTestModule : AbpModule
    {

    }
}