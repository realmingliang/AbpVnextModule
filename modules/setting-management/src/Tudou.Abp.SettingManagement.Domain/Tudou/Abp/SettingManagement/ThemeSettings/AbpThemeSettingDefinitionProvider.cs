using System;
using System.Collections.Generic;
using System.Text;
using Tudou.Abp.SettingManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tudou.Abp.SettingManagement.ThemeSettings
{
    public class AbpThemeSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
               new SettingDefinition(
                   ThemeSettingNames.NavTheme,
                   "dark",
                   L("DisplayName:Abp.Theme.NavTheme"),
                   L("Description:Abp.Theme.NavTheme"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.PrimaryColor,
                   "#1890ff",
                   L("DisplayName:Abp.Theme.NavTheme"),
                   L("Description:Abp.Theme.NavTheme"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.Layout,
                   "sidemenu",
                   L("DisplayName:Abp.Theme.Layout"),
                   L("Description:Abp.Theme.Layout"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.ContentWidth,
                   "Fixed",
                   L("DisplayName:Abp.Theme.ContentWidth"),
                   L("Description:Abp.Theme.ContentWidth"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.FixedHeader,
                   false.ToString(),
                   L("DisplayName:Abp.Theme.FixedHeader"),
                   L("Description:Abp.Theme.FixedHeader"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.AutoHideHeader,
                   false.ToString(),
                   L("DisplayName:Abp.Theme.AutoHideHeader"),
                   L("Description:Abp.Theme.AutoHideHeader"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.FixSiderbar,
                   false.ToString(),
                   L("DisplayName:Abp.Theme.FixSiderbar"),
                   L("Description:Abp.Theme.FixSiderbar"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.ColorWeak,
                   false.ToString(),
                   L("DisplayName:Abp.Theme.ColorWeak"),
                   L("Description:Abp.Theme.ColorWeak"),
                   true),
               new SettingDefinition(
                   ThemeSettingNames.Title,
                   "Ant Design Pro",
                   L("DisplayName:Abp.Theme.Title"),
                   L("Description:Abp.Theme.Title"),
                   true));
        }
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSettingManagementResource>(name);
        }
    }
}
