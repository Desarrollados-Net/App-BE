using Volo.Abp.Modularity;

namespace SeriesPlus;

/* Inherit from this class for your domain layer tests. */
public abstract class SeriesPlusDomainTestBase<TStartupModule> : SeriesPlusTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
