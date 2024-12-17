using Application.Abstractions.Data;
using Domain.Categories;
using Domain.Ingredients;
using Domain.Recipes;
using Domain.Tags;
using Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

//Set as Startup project: API
//Default project PM: Infrastructure
//Add-Migration Initial -Context AppWriteDbContext
//Update-Database -Context AppWriteDbContext
public sealed class AppWriteDbContext : DbContext, IUnitOfWork
{
    public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options) : base(options) { }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeStep> Steps { get; set; }
    public DbSet<RecipeTag> RecipeTags { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
