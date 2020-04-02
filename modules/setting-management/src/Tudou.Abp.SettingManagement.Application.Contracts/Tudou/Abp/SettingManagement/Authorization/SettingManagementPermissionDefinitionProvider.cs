using Tudou.Abp.Identity;
using Tudou.Abp.SettingManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tudou.Abp.SettingManagement.Authorization
{
    public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroupOrNull(IdentityPermissions.GroupName);
            identityGroup.AddPermission(SettingManagementPermissions.IdentitySettingsManagement, L("SettingsManageMent"));
            var themeGroup = context.AddGroup(SettingManagementPermissions.Theme.Default, L("Theme"));
            themeGroup.AddPermission(SettingManagementPermissions.Theme.SettingsManagement, L("SettingsManageMent"));
            var accountGroup = context.AddGroup(SettingManagementPermissions.Account.Default, L("Account"));
            accountGroup.AddPermission(SettingManagementPermissions.Account.SettingsManagement, L("SettingsManageMent"));
        }
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSettingManagementResource>(name);
        }
    }
}
