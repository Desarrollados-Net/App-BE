using Volo.Abp.Settings;

namespace SeriesPlus.Settings;

public class SeriesPlusSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SeriesPlusSettings.MySetting1));
    }
}
