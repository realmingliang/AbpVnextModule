using Tudou.Abp.Saas.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.Saas
{
    public class AbpSaasPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var saasGroup = context.AddGroup(SaasPermissions.GroupName, L("Permission:Saas"));

            var tenantsPermission = saasGroup.AddPermission(SaasPermissions.Tenants.Default, L("Permission:TenantManagement"), multiTenancySide: MultiTenancySides.Host);
            tenantsPermission.AddChild(SaasPermissions.Tenants.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Host);
            tenantsPermission.AddChild(SaasPermissions.Tenants.Update, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Host);
            tenantsPermission.AddChild(SaasPermissions.Tenants.Delete, L("Permission:Delete"), multiTenancySide: MultiTenancySides.Host);
            tenantsPermission.AddChild(SaasPermissions.Tenants.ManageFeatures, L("Permission:ManageFeatures"), multiTenancySide: MultiTenancySides.Host);
            tenantsPermission.AddChild(SaasPermissions.Tenants.ManageConnectionStrings, L("Permission:ManageConnectionStrings"), multiTenancySide: MultiTenancySides.Host);

            var editionsPermission = saasGroup.AddPermission(SaasPermissions.Editions.Default, L("Permission:EditionManagement"), multiTenancySide: MultiTenancySides.Host);
            editionsPermission.AddChild(SaasPermissions.Editions.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Host);
            editionsPermission.AddChild(SaasPermissions.Editions.Update, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Host);
            editionsPermission.AddChild(SaasPermissions.Editions.Delete, L("Permission:Delete"), multiTenancySide: MultiTenancySides.Host);
            editionsPermission.AddChild(SaasPermissions.Editions.ManageFeatures, L("Permission:ManageFeatures"), multiTenancySide: MultiTenancySides.Host);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSaasResource>(name);
        }
    }
}
