using Tudou.Abp.OrganizationUnit.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tudou.Abp.OrganizationUnit.Authorization
{
    public class OrganizationUnitPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var organizationUnitGroup = context.AddGroup(OrganizationUnitPermissions.GroupName, L("Permission:OrganizationUnit"));
            var organizationUnitPermission = organizationUnitGroup.AddPermission(OrganizationUnitPermissions.OrganizationUnit.Default, L("Permission:OrganizationUnit"));
            organizationUnitPermission.AddChild(OrganizationUnitPermissions.OrganizationUnit.Create, L("Permission:Create"));
            organizationUnitPermission.AddChild(OrganizationUnitPermissions.OrganizationUnit.Update, L("Permission:Update"));
            organizationUnitPermission.AddChild(OrganizationUnitPermissions.OrganizationUnit.Move, L("Permission:Move"));
            organizationUnitPermission.AddChild(OrganizationUnitPermissions.OrganizationUnit.Delete, L("Permission:Delete"));
            var organizationUnitUserPermission = organizationUnitGroup.AddPermission(OrganizationUnitPermissions.OrganizationUnitUser.Default, L("Permission:OrganizationUnitUser"));
            organizationUnitUserPermission.AddChild(OrganizationUnitPermissions.OrganizationUnitUser.AddUsersToOrganizationUnit, L("Permission:AddUsersToOrganizationUnit"));
            organizationUnitUserPermission.AddChild(OrganizationUnitPermissions.OrganizationUnitUser.RemoveUserFromOrganizationUnit, L("Permission:RemoveUserFromOrganizationUnit"));
            organizationUnitUserPermission.AddChild(OrganizationUnitPermissions.OrganizationUnitUser.FindUsers, L("Permission:FindUsers"));
            var organizationUnitRolePermission = organizationUnitGroup.AddPermission(OrganizationUnitPermissions.OrganizationUnitRole.Default, L("Permission:OrganizationUnitRole"));
            organizationUnitRolePermission.AddChild(OrganizationUnitPermissions.OrganizationUnitRole.AddRolesToOrganizationUnit, L("Permission:AddRolesToOrganizationUnit"));
            organizationUnitRolePermission.AddChild(OrganizationUnitPermissions.OrganizationUnitRole.FindRoles, L("Permission:FindRoles"));
            organizationUnitRolePermission.AddChild(OrganizationUnitPermissions.OrganizationUnitRole.RemoveRoleFromOrganizationUnit, L("Permission:RemoveRoleFromOrganizationUnit"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrganizationUnitResource>(name);
        }
    }
}