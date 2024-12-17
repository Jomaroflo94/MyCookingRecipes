using Domain.Categories;
using Domain.Ingredients;
using Domain.Recipes;
using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Ingredient.Name))
                .HasMaxLength(100)
                .HasColumnName(nameof(Ingredient.Name).ToLower())
                .IsRequired());

        builder.ComplexProperty(
            u => u.Quantity,
            b => b.Property(e => e.Value).HasColumnName(nameof(Ingredient.Quantity))
                .HasColumnType("decimal(10, 2)")
                .HasColumnName(nameof(Ingredient.Quantity).ToLower())
                .IsRequired());

        builder.Property(t => t.CreatedOnUtc).IsRequired();

        builder.HasOne<Tag>()
              .WithOne()
              .HasForeignKey<Ingredient>(s => s.TagId);

        builder.HasMany<RecipeIngredient>()
            .WithOne()
            .HasForeignKey(rt => rt.IngredientId);

        builder.HasMany<Category>()
              .WithMany(s => s.Ingredients)
              .UsingEntity(j => j.ToTable("IngredientCategories"));
    }
}
