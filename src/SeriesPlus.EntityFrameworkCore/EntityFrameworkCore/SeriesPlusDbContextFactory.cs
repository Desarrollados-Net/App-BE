using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SeriesPlus.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SeriesPlusDbContextFactory : IDesignTimeDbContextFactory<SeriesPlusDbContext>
{
    public SeriesPlusDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        SeriesPlusEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<SeriesPlusDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new SeriesPlusDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SeriesPlus.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
