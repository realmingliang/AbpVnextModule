using System;
using System.Collections.Generic;
using System.Text;
using Tudou.Abp.AuditLogging;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.AuditLogging
{
    public class AuditLoggingAppServiceBase: ApplicationService
    {
        protected AuditLoggingAppServiceBase()
        {
            ObjectMapperContext = typeof(AbpAuditLoggingApplicationModule);
        }
    }
}
