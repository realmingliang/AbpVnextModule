using Volo.Abp.Application.Services;
using Tudou.Abp.Saas.Localization;

namespace Tudou.Abp.Saas
{
    public abstract class SaasAppServiceBase : ApplicationService
    {
        protected SaasAppServiceBase()
        {
            ObjectMapperContext = typeof(AbpSaasApplicationModule);
            LocalizationResource = typeof(AbpSaasResource);
        }
    }
}