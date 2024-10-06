using System.Threading.Tasks;

namespace SeriesPlus.Data;

public interface ISeriesPlusDbSchemaMigrator
{
    Task MigrateAsync();
}
