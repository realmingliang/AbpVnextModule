using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;
using Tudou.Abp.SettingManagement.Localization;
using Volo.Abp.Identity;

namespace Tudou.Abp.SettingManagement
{
    [DependsOn(typeof(AbpLocalizationModule),
              typeof(AbpIdentityDomainSharedModule))]
    public class AbpSettingManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpSettingManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpSettingManagementResource>("en")
                    .AddVirtualJson("/Tudou/Abp/SettingManagement/Localization");
            });
        }
    }
}
