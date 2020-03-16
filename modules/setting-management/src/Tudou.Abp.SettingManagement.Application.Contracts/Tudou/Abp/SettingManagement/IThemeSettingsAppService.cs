using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tudou.Abp.SettingManagement
{
    public interface IThemeSettingsAppService
    {
        Task<ThemeSettingsDto> GetAllSettingsAsync();

        Task UpdateAllSettingsAsync(UpdateThemeSettingsInput input);
    }
}
