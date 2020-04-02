using System;
using System.Collections.Generic;
using System.Text;
using Tudou.Grace.Localization;
using Volo.Abp.Application.Services;

namespace Tudou.Grace
{
    /* Inherit your application services from this class.
     */
    public abstract class GraceAppService : ApplicationService
    {
        protected GraceAppService()
        {
            LocalizationResource = typeof(GraceResource);
        }
    }
}
