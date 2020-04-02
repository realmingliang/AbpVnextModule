using Tudou.Grace.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tudou.Grace.Permissions
{
    public class GracePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(GracePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(GracePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<GraceResource>(name);
        }
    }
}
