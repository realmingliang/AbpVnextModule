using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tudou.Abp.SettingManagement.Authorization;
using Tudou.Abp.Identity.Settings;
using Volo.Abp.Settings;

namespace Tudou.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.IdentitySettingsManagement)]
    public class IdentitySettingsAppService : SettingManagementAppServiceBase, IIdentitySettingsAppService
    {
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingManager _settingManager;
        public IdentitySettingsAppService(ISettingProvider settingProvider,
            ISettingManager settingManager)
        {
            _settingProvider = settingProvider;
            _settingManager = settingManager;
        }
        public async Task<IdentitySettingsDto> GetAllSettingsAsync()
        {
            return new IdentitySettingsDto()
            {
                Password = await GetPasswordSettingsAsync(),
                LockOut = await GetLockOutSettingsAsync(),
                SignIn = await GetSignInSettingsAsync(),
                User = await GetUserSettingsAsync(),
            };
        }
        private async Task<IdentitySettingsUserDto> GetUserSettingsAsync()
        {
            return new IdentitySettingsUserDto()
            {
                IsEmailUpdateEnabled = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.User.IsEmailUpdateEnabled)),
                IsUserNameUpdateEnabled = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.User.IsUserNameUpdateEnabled)),
            };
        }
        private async Task<IdentitySettingsSignInDto> GetSignInSettingsAsync()
        {
            return new IdentitySettingsSignInDto()
            {
                RequireConfirmedEmail = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.SignIn.RequireConfirmedEmail)),
                EnablePhoneNumberConfirmation = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.SignIn.EnablePhoneNumberConfirmation)),
                RequireConfirmedPhoneNumber = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.SignIn.RequireConfirmedPhoneNumber)),
            };
        }
        private async Task<IdentitySettingsLockOutDto> GetLockOutSettingsAsync()
        {
            return new IdentitySettingsLockOutDto()
            {
                AllowedForNewUsers = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Lockout.AllowedForNewUsers)),
                LockoutDuration = int.Parse(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Lockout.LockoutDuration)),
                MaxFailedAccessAttempts = int.Parse(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Lockout.MaxFailedAccessAttempts)),

            };
        }
        private async Task<IdentitySettingsPasswordDto> GetPasswordSettingsAsync()
        {

            return new IdentitySettingsPasswordDto()
            {
                RequireDigit = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequireDigit)),
                RequiredLength = int.Parse(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequiredLength)),
                RequiredUniqueChars = int.Parse(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequiredUniqueChars)),
                RequireLowercase = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequireLowercase)),
                RequireNonAlphanumeric = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequireNonAlphanumeric)),
                RequireUppercase = Convert.ToBoolean(await _settingProvider.GetOrNullAsync(IdentitySettingNames.Password.RequireUppercase)),
            };
        }
        public async Task UpdateAllSettingsAsync(UpdateIdentitySettingsInput input)
        {
            await UpdateUserSettingsAsync(input.User);
            await UpdatePasswordSettingsAsync(input.Password);
            await UpdateSignInSettingsAsync(input.SignIn);
            await UpdateLockOutSettingsAsync(input.LockOut);
        }

        private async Task UpdateUserSettingsAsync(IdentitySettingsUserDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.User.IsEmailUpdateEnabled, input.IsEmailUpdateEnabled.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.User.IsUserNameUpdateEnabled, input.IsUserNameUpdateEnabled.ToString());

            }
            else
            {
                await _settingManager.SetGlobalAsync(IdentitySettingNames.User.IsEmailUpdateEnabled, input.IsEmailUpdateEnabled.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.User.IsUserNameUpdateEnabled, input.IsUserNameUpdateEnabled.ToString());

            }
        }
        private async Task UpdateSignInSettingsAsync(IdentitySettingsSignInDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.SignIn.RequireConfirmedEmail, input.RequireConfirmedEmail.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.SignIn.RequireConfirmedPhoneNumber, input.RequireConfirmedPhoneNumber.ToString());

            }
            else
            {
                await _settingManager.SetGlobalAsync(IdentitySettingNames.SignIn.RequireConfirmedEmail, input.RequireConfirmedEmail.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.SignIn.RequireConfirmedPhoneNumber, input.RequireConfirmedPhoneNumber.ToString());
            }
        }
        private async Task UpdateLockOutSettingsAsync(IdentitySettingsLockOutDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Lockout.AllowedForNewUsers, input.AllowedForNewUsers.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Lockout.LockoutDuration, input.LockoutDuration.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Lockout.MaxFailedAccessAttempts, input.MaxFailedAccessAttempts.ToString());

            }
            else
            {
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Lockout.AllowedForNewUsers, input.AllowedForNewUsers.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Lockout.LockoutDuration, input.LockoutDuration.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Lockout.MaxFailedAccessAttempts, input.MaxFailedAccessAttempts.ToString());
            }
        }
        private async Task UpdatePasswordSettingsAsync(IdentitySettingsPasswordDto input)
        {
            if (CurrentTenant.Id.HasValue)
            {
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequireDigit, input.RequireDigit.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequiredLength, input.RequiredLength.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequiredUniqueChars, input.RequiredUniqueChars.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequireLowercase, input.RequireLowercase.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequireNonAlphanumeric, input.RequireNonAlphanumeric.ToString());
                await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, IdentitySettingNames.Password.RequireUppercase, input.RequireUppercase.ToString());

            }
            else
            {
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequireDigit, input.RequireDigit.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequiredLength, input.RequiredLength.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequiredUniqueChars, input.RequiredUniqueChars.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequireLowercase, input.RequireLowercase.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequireNonAlphanumeric, input.RequireNonAlphanumeric.ToString());
                await _settingManager.SetGlobalAsync(IdentitySettingNames.Password.RequireUppercase, input.RequireUppercase.ToString());
            }
        }
    }
}
