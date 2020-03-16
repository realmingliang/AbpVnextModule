namespace Tudou.Abp.SettingManagement
{
    public class UpdateAccountSettingsInput
    {
        public bool IsSelfRegistrationEnabled { get; set; }

        public bool EnableLocalLogin { get; set; }
    }
}