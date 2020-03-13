using Tudou.Abp.OrganizationUnit.Localization;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.OrganizationUnit
{
    public abstract class OrganizationUnitAppServiceBase : ApplicationService
    {
        protected OrganizationUnitAppServiceBase()
        {
            LocalizationResource = typeof(OrganizationUnitResource);
            ObjectMapperContext = typeof(OrganizationUnitApplicationModule);
        }
    }
}
