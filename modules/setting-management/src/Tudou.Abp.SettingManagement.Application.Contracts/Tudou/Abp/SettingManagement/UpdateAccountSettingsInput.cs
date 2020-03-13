namespace Tudou.Abp.SettingManagement
{
    public class UpdateAccountSettingsInput
    {
        public bool IsSelfRegistrationEnabled { get; set; }

        public bool EnableLocalLogin { get; set; }

        public bool IsRememberBrowserEnabled { get; set; }
    }
}