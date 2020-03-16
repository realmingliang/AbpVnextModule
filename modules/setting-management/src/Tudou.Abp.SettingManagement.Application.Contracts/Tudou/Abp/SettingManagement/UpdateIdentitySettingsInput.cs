using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.SettingManagement
{
    public class UpdateIdentitySettingsInput
    {
        public IdentitySettingsPasswordDto Password { get; set; }

        public IdentitySettingsLockOutDto LockOut { get; set; }

        public IdentitySettingsSignInDto SignIn { get; set; }
        public IdentitySettingsUserDto User { get; set; }
    }
}