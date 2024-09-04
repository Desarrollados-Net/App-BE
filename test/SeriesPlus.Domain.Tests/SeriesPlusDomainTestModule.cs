using Volo.Abp.Modularity;

namespace SeriesPlus;

[DependsOn(
    typeof(SeriesPlusDomainModule),
    typeof(SeriesPlusTestBaseModule)
)]
public class SeriesPlusDomainTestModule : AbpModule
{

}
