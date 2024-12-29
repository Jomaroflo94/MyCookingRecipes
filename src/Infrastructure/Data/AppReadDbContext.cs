using Domain.Recipes;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal sealed class AppReadDbContext : DbContext
{
    public AppReadDbContext(DbContextOptions<AppReadDbContext> options) : base(options) { }

    public DbSet<TagRead> Tags { get; set; }
    public DbSet<RecipeRead> Recipes { get; set; }
    public DbSet<RecipeIngredientRead> RecipeIngredients { get; set; }
    public DbSet<RecipeStepRead> Steps { get; set; }
    public DbSet<IngredientRead> Ingredients { get; set; }
    public DbSet<UnitRead> Units { get; set; }
    public DbSet<CategoryRead> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppWriteDbContext).Assembly,
            ReadConfigurationsFilter);
    }

    private static bool ReadConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;
}
