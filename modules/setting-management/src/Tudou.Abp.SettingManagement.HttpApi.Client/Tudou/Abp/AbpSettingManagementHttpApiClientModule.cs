using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Tudou.Abp.SettingManagement
{
    [DependsOn(
          typeof(AbpSettingManagementApplicationContractsModule),
          typeof(AbpHttpClientModule))]
    public class AbpSettingManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "AbpSettingManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpSettingManagementHttpApiClientModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
