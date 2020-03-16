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
    [Route("api/account/settings")]
    public class AccountSettingsController : AbpController, IAccountSettingsAppService
    {
        private readonly IAccountSettingsAppService _accountSettingAppService;

        public AccountSettingsController(IAccountSettingsAppService accountSettingAppService)
        {
            _accountSettingAppService = accountSettingAppService;
        }
        [HttpGet]
        public async Task<AccountSettingsDto> GetAllSettingsAsync()
        {
            return await _accountSettingAppService.GetAllSettingsAsync();
        }
        [HttpPost]
        public async Task UpdateAllSettingsAsync(UpdateAccountSettingsInput input)
        {
            await _accountSettingAppService.UpdateAllSettingsAsync(input);
        }
    }
}
