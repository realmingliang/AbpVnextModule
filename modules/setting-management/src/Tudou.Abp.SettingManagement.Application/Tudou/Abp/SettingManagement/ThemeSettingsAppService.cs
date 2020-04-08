using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Tudou.Abp.SettingManagement.Authorization;
using Tudou.Abp.SettingManagement.ThemeSettings;
using Volo.Abp.Settings;

namespace Tudou.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.Theme.SettingsManagement)]
    public class ThemeSettingsAppService : SettingManagementAppServiceBase, IThemeSettingsAppService
    {
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingManager _settingManager;
        public ThemeSettingsAppService(ISettingProvider settingProvider,
            ISettingManager settingManager)
        {
            _settingProvider = settingProvider;
            _settingManager = settingManager;
        }
        public async Task<ThemeSettingsDto> GetAllSettingsAsync()
        {
            return new ThemeSettingsDto
            {
                NavTheme = await _settingProvider.GetOrNullAsync(ThemeSettingNames.NavTheme),
                Layout = await _settingProvider.GetOrNullAsync(ThemeSettingNames.Layout),
                ContentWidth = await _settingProvider.GetOrNullAsync(ThemeSettingNames.ContentWidth),
                FixedHeader = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(ThemeSettingNames.FixedHeader)),
                AutoHideHeader = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(ThemeSettingNames.AutoHideHeader)),
                FixSiderbar = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(ThemeSettingNames.FixSiderbar)),
                ColorWeak = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(ThemeSettingNames.ColorWeak)),
            };
        }

        public async Task UpdateAllSettingsAsync(UpdateThemeSettingsInput input)
        {
            if (CurrentUser.Id != null)
            {
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.NavTheme, input.NavTheme);
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.Layout, input.Layout);
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.ContentWidth,
                    input.ContentWidth);
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.FixedHeader,
                    input.FixedHeader.ToString());
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.AutoHideHeader,
                    input.AutoHideHeader.ToString());
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.FixSiderbar,
                    input.FixSiderbar.ToString());
                await _settingManager.SetForUserAsync(CurrentUser.Id.Value, ThemeSettingNames.ColorWeak,
                    input.ColorWeak.ToString());
            }
        }
    }
}
