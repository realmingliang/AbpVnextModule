using Volo.Abp.Settings;

namespace Tudou.Grace.Settings
{
    public class GraceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(GraceSettings.MySetting1));
        }
    }
}
