using SeriesPlus.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SeriesPlus.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SeriesPlusController : AbpControllerBase
{
    protected SeriesPlusController()
    {
        LocalizationResource = typeof(SeriesPlusResource);
    }
}
