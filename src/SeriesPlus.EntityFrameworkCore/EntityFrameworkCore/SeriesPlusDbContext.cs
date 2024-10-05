using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using SeriesPlus.Series;
using SeriesPlus.WatchLists;                              //agregue esto para q se compile
//using SeriesPlus.Domain.Series; // esto no funciono


namespace SeriesPlus.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class SeriesPlusDbContext :
    AbpDbContext<SeriesPlusDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. 
     (apartir de Aqui debo agregar los Agreggate root) */
    public DbSet<Serie> Series { get; set;}
    public DbSet<WatchList> WatchLists{ get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public SeriesPlusDbContext(DbContextOptions<SeriesPlusDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.Entity<Serie>(b =>
        {
            b.ToTable(SeriesPlusConsts.DbTablePrefix + "Series",
                SeriesPlusConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Titulo).IsRequired().HasMaxLength(128);
            b.Property(x => x.Genero).IsRequired().HasMaxLength(128);
            b.Property(x => x.FechaLanzamiento).IsRequired(); //no se restringe xq es unafecha 
            b.Property(x => x.Duracion).IsRequired().HasMaxLength(128);//no hace falta restrin. xq puede tomar cualq.valor Num.
            b.Property(x => x.FotoPortada).HasMaxLength(256); //ruta
            b.Property(x => x.Idioma).IsRequired().HasMaxLength(256);
            b.Property(x => x.PaisOrigen).IsRequired().HasMaxLength(64);
            b.Property(x => x.CalificacionIMBD).IsRequired().HasMaxLength(128);
            b.Property(x => x.Director).IsRequired().HasMaxLength(128);
            b.Property(x => x.Escritor).HasMaxLength(128);
            b.Property(x => x.Actores).HasMaxLength(256);
         //   b.Property(x => x.If_Emision).IsRequired();//un boolean no hace falta restringir(2 valores posib.)
        });

        builder.Entity<WatchList>(b =>
        {
            b.ToTable(SeriesPlusConsts.DbTablePrefix + "WatchList",
                SeriesPlusConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();
        
        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(SeriesPlusConsts.DbTablePrefix + "YourEntities", SeriesPlusConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
