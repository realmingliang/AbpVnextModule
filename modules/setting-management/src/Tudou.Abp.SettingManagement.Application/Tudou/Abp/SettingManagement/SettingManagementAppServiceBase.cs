using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.SettingManagement
{
   public class SettingManagementAppServiceBase: ApplicationService
    {
        protected SettingManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(AbpSettingManagementApplicationModule);
        }
    }
}
