using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.SettingManagement
{
    [RemoteService]
    [Area("settings")]
    [ControllerName("Settings")]
    [Route("api/themes/settings")]
    public class ThemeSettingsController : AbpController, IThemeSettingsAppService
    {
        private readonly IThemeSettingsAppService _themeSettingAppService;

        public ThemeSettingsController(IThemeSettingsAppService themeSettingAppService)
        {
            _themeSettingAppService = themeSettingAppService;
        }
        [HttpGet]
        public async Task<ThemeSettingsDto> GetAllSettingsAsync()
        {
            return await _themeSettingAppService.GetAllSettingsAsync();
        }
        [HttpPost]
        public async Task UpdateAllSettingsAsync(UpdateThemeSettingsInput input)
        {
             await _themeSettingAppService.UpdateAllSettingsAsync(input);
        }
    }
}
