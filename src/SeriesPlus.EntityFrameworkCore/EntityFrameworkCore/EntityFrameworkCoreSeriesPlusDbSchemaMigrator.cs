using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeriesPlus.Data;
using Volo.Abp.DependencyInjection;

namespace SeriesPlus.EntityFrameworkCore;

public class EntityFrameworkCoreSeriesPlusDbSchemaMigrator
    : ISeriesPlusDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSeriesPlusDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SeriesPlusDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SeriesPlusDbContext>()
            .Database
            .MigrateAsync();
    }
}
