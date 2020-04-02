using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Tudou.Abp.Account.Settings;
using Tudou.Abp.SettingManagement.Authorization;
using Volo.Abp.Settings;

namespace Tudou.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.Account.SettingsManagement)]
    public class AccountSettingsAppService : SettingManagementAppServiceBase, IAccountSettingsAppService
    {
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingManager _settingManager;
        public AccountSettingsAppService(ISettingProvider settingProvider,
            ISettingManager settingManager)
        {
            _settingProvider = settingProvider;
            _settingManager = settingManager;
        }

        public async Task<AccountSettingsDto> GetAllSettingsAsync()
        {
            return new AccountSettingsDto
            {
                IsSelfRegistrationEnabled = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(AccountSettingNames.IsSelfRegistrationEnabled)),
                EnableLocalLogin = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(AccountSettingNames.EnableLocalLogin)),
            };
        }

        public async Task UpdateAllSettingsAsync(UpdateAccountSettingsInput input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value,AccountSettingNames.IsSelfRegistrationEnabled, input.IsSelfRegistrationEnabled.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value,AccountSettingNames.EnableLocalLogin, input.EnableLocalLogin.ToString());
            }
            else {
                await _settingManager.SetGlobalAsync(AccountSettingNames.IsSelfRegistrationEnabled, input.IsSelfRegistrationEnabled.ToString());
                await _settingManager.SetGlobalAsync(AccountSettingNames.EnableLocalLogin, input.EnableLocalLogin.ToString());
            }
           
        }
    }
}
