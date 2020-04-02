using Tudou.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tudou.Abp.Identity.Web.Pages.Identity
{
    public abstract class IdentityPageModel : AbpPageModel
    {
        protected IdentityPageModel()
        {
            ObjectMapperContext = typeof(AbpIdentityWebModule);
        }
    }
}
