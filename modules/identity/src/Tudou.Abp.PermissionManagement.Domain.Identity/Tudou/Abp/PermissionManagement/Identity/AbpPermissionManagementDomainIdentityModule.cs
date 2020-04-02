using Volo.Abp.Authorization.Permissions;
using Tudou.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.PermissionManagement.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpPermissionManagementDomainModule)
        )]
    public class AbpPermissionManagementDomainIdentityModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.ManagementProviders.Add<UserPermissionManagementProvider>();
                options.ManagementProviders.Add<RolePermissionManagementProvider>();
                options.ManagementProviders.Add<OrganizationUnitPermissionManagementPrivider>();


                //TODO: Can we prevent duplication of permission names without breaking the design and making the system complicated
                options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = "AbpIdentity.Users.ManagePermissions";
                options.ProviderPolicies[OrganizationUnitPermissionManagementPrivider.ProviderName] = "AbpIdentity.OrganizationUnit.ManagePermissions";
                options.ProviderPolicies[RolePermissionValueProvider.ProviderName] = "AbpIdentity.Roles.ManagePermissions";
            });
        }
    }
}
