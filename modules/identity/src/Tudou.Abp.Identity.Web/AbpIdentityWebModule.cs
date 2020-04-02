using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.AspNetCore.Mvc.Localization;
using Tudou.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Tudou.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Tudou.Abp.AutoMapper;
using Tudou.Abp.Identity.Localization;
using Tudou.Abp.Identity.Web.Navigation;
using Tudou.Abp.Modularity;
using Tudou.Abp.PermissionManagement.Web;
using Tudou.Abp.UI.Navigation;
using Tudou.Abp.VirtualFileSystem;

namespace Tudou.Abp.Identity.Web
{
    [DependsOn(typeof(AbpIdentityHttpApiModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiBootstrapModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpPermissionManagementWebModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiThemeSharedModule))]
    public class AbpIdentityWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(IdentityResource), typeof(AbpIdentityWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpIdentityWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AbpIdentityWebMainMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpIdentityWebModule>("Tudou.Abp.Identity.Web");
            });

            context.Services.AddAutoMapperObjectMapper<AbpIdentityWebModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpIdentityWebAutoMapperProfile>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Identity/Users/Index", IdentityPermissions.Users.Default);
                options.Conventions.AuthorizePage("/Identity/Users/CreateModal", IdentityPermissions.Users.Create);
                options.Conventions.AuthorizePage("/Identity/Users/EditModal", IdentityPermissions.Users.Update);
                options.Conventions.AuthorizePage("/Identity/Roles/Index", IdentityPermissions.Roles.Default);
                options.Conventions.AuthorizePage("/Identity/Roles/CreateModal", IdentityPermissions.Roles.Create);
                options.Conventions.AuthorizePage("/Identity/Roles/EditModal", IdentityPermissions.Roles.Update);
            });
        }
    }
}
