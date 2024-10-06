using SeriesPlus.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SeriesPlus.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SeriesPlusEntityFrameworkCoreModule),
    typeof(SeriesPlusApplicationContractsModule)
)]
public class SeriesPlusDbMigratorModule : AbpModule
{
}
