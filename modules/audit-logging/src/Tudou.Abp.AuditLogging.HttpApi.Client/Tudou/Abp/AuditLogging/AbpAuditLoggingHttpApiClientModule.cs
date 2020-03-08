using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(
          typeof(AbpAuditLoggingApplicationContractsModule),
          typeof(AbpHttpClientModule))]
    public class AbpAuditLoggingHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "AbpAuditLogging";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpAuditLoggingApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
