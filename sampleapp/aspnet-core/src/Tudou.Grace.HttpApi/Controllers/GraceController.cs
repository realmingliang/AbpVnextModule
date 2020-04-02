using Tudou.Grace.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Grace.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class GraceController : AbpController
    {
        protected GraceController()
        {
            LocalizationResource = typeof(GraceResource);
        }
    }
}