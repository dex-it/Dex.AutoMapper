using Microsoft.EntityFrameworkCore;

namespace DAL.EFCore;

public sealed class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<TMandator> MandatorSet { get; set; }

    public DbSet<TBuilding> BuildingSet { get; set; }

    public DbSet<TBuilder> Builder { get; set; }

    public DbSet<TCity> City { get; set; }
}