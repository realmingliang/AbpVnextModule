namespace Tudou.Abp.SettingManagement
{
    public class ThemeSettingsDto
    {
        public string NavTheme { get; set; }

        public string PrimaryColor { get; set; }

        public string Layout { get; set; }

        public string ContentWidth { get; set; }

        public bool FixedHeader { get; set; }
        public bool AutoHideHeader { get; set; }

        public bool FixSiderbar { get; set; }
        /// <summary>
        /// 色弱
        /// </summary>
        public bool ColorWeak { get; set; }

    }
}