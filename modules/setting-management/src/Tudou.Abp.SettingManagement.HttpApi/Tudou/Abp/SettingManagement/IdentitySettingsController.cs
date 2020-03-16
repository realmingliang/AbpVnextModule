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
    [Route("api/identity/settings")]
    public class IdentitySettingsController : AbpController, IIdentitySettingsAppService
    {
        private readonly IIdentitySettingsAppService _identitySettingAppService;

        public IdentitySettingsController(IIdentitySettingsAppService identitySettingAppService)
        {
            _identitySettingAppService = identitySettingAppService;
        }
        [HttpGet]
        public async Task<IdentitySettingsDto> GetAllSettingsAsync()
        {
            return await _identitySettingAppService.GetAllSettingsAsync();
        }
        [HttpPost]
        public async Task UpdateAllSettingsAsync(UpdateIdentitySettingsInput input)
        {
             await _identitySettingAppService.UpdateAllSettingsAsync(input);
        }
    }
}
