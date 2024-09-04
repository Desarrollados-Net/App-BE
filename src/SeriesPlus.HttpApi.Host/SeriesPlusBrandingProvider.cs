using Microsoft.Extensions.Localization;
using SeriesPlus.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SeriesPlus;

[Dependency(ReplaceServices = true)]
public class SeriesPlusBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SeriesPlusResource> _localizer;

    public SeriesPlusBrandingProvider(IStringLocalizer<SeriesPlusResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
