using System;
using System.Collections.Generic;
using System.Text;

namespace Tudou.Abp.SettingManagement.ThemeSettings
{
    public static class ThemeSettingNames
    {
        private const string Prefix = "Abp.Theme";
        public const string NavTheme = Prefix + ".NavTheme";
        public const string PrimaryColor = Prefix + ".PrimaryColor";
        public const string Layout = Prefix + ".Layout";
        public const string ContentWidth = Prefix + ".ContentWidth";
        public const string FixedHeader = Prefix + ".FixedHeader";
        public const string AutoHideHeader = Prefix + ".AutoHideHeader";
        public const string FixSiderbar = Prefix + ".FixSiderbar";
        public const string ColorWeak = Prefix + ".ColorWeak";
        public const string Title = Prefix + ".Title";
    }
}
