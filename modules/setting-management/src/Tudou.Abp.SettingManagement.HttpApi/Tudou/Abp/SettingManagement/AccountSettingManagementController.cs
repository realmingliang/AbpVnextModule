using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.SettingManagement
{
    [RemoteService]
    [Area("account")]
    [ControllerName("Settings")]
    [Route("api/audit-logging/settings")]
    public class AccountSettingManagementController : AbpController, IAccountSettingManagementAppService
    {
        private readonly IAccountSettingManagementAppService _accountSettingAppService;

        public AccountSettingManagementController(IAccountSettingManagementAppService accountSettingAppService)
        {
            _accountSettingAppService = accountSettingAppService;
        }

        [HttpGet]
        public async Task<AccountSettingsDto> GetAllSettingsAsync()
        {
            return await _accountSettingAppService.GetAllSettingsAsync();
        }

        [HttpPut]
        public async Task UpdateAllSettingsAsync(UpdateAccountSettingsInput input)
        {
             await _accountSettingAppService.UpdateAllSettingsAsync(input);
        }
    }
}
