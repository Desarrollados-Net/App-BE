using Volo.Abp.Modularity;

namespace SeriesPlus;

public abstract class SeriesPlusApplicationTestBase<TStartupModule> : SeriesPlusTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
