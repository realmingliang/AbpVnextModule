using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tudou.Abp.SettingManagement
{
   public interface IIdentitySettingsAppService
    {
        Task<IdentitySettingsDto> GetAllSettingsAsync();

        Task UpdateAllSettingsAsync(UpdateIdentitySettingsInput input);
    }
}
