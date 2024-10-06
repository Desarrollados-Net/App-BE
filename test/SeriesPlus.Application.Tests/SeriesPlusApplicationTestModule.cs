using Volo.Abp.Modularity;

namespace SeriesPlus;

[DependsOn(
    typeof(SeriesPlusApplicationModule),
    typeof(SeriesPlusDomainTestModule)
)]
public class SeriesPlusApplicationTestModule : AbpModule
{

}
