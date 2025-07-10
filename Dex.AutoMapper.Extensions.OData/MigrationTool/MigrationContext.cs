using DAL.EFCore;
using Microsoft.EntityFrameworkCore;

namespace MigrationTool;

public class MigrationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=YourDB;Connection Timeout=300"
        );
    }

    public DbSet<TMandator> MandatorSet { get; set; }

    public DbSet<TBuilding> BuildingSet { get; set; }

    public DbSet<TBuilder> Builder { get; set; }

    public DbSet<TCity> City { get; set; }
}