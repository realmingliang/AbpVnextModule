using Tudou.Abp.Identity;
namespace Tudou.Abp.SettingManagement.Authorization
{
    public class SettingManagementPermissions
    {
        public const string Name = ".SettingManagement";
       
        public const string IdentitySettingsManagement = IdentityPermissions.GroupName + Name;

        public class Account {
            public const string Default = "AbpAccount";
            public const string SettingsManagement = Default + Name;
        }
        public class Theme
        {
            public const string Default = "AbpTheme";
            public const string SettingsManagement = Default + Name;
        }
    }
}
