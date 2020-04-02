using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tudou.Grace.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(GraceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class GraceConsoleApiClientModule : AbpModule
    {
        
    }
}
