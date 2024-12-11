using Domain.Ingredients;
using Domain.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Recipe.Name))
                .HasMaxLength(100)
                .HasColumnName(nameof(Recipe.Name).ToLower())
                .IsRequired());

        builder.ComplexProperty(
            u => u.Description,
            b => b.Property(e => e.Value).HasColumnName(nameof(Recipe.Description))
                .HasMaxLength(500)
                .HasColumnName(nameof(Recipe.Description).ToLower())
                .IsRequired());

        builder.Property(t => t.CreatedOnUtc).IsRequired();

        builder.HasMany<RecipeIngredient>()
              .WithOne()
              .HasForeignKey(i => i.RecipeId);

        builder.HasMany<RecipeStep>()
              .WithOne()
              .HasForeignKey(s => s.RecipeId);

        builder.HasMany<RecipeTag>()
              .WithOne()
              .HasForeignKey(rt => rt.RecipeId);
    }
}
