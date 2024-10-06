using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SeriesPlus.Data;

/* This is used if database provider does't define
 * ISeriesPlusDbSchemaMigrator implementation.
 */
public class NullSeriesPlusDbSchemaMigrator : ISeriesPlusDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
