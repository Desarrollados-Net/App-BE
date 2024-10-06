using SeriesPlus.Localization;
using Volo.Abp.Application.Services;

namespace SeriesPlus;

/* Inherit your application services from this class.
 */
public abstract class SeriesPlusAppService : ApplicationService
{
    protected SeriesPlusAppService()
    {
        LocalizationResource = typeof(SeriesPlusResource);
    }
}
