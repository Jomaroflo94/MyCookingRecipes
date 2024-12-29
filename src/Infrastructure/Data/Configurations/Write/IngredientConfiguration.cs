using Domain.Categories;
using Domain.Ingredients;
using Domain.Recipes;
using Domain.Tags;
using Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.Property(p => p.TagId)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Ingredient.Name))
                .HasMaxLength(100)
                .HasColumnName(nameof(Ingredient.Name).ToLower())
                .IsRequired());

        builder.HasOne<Tag>()
            .WithOne()
            .HasForeignKey<Ingredient>(s => s.TagId);

        builder.HasMany<RecipeIngredient>()
            .WithOne()
            .HasForeignKey(rt => rt.IngredientId);

        builder.HasMany(s => s.Categories)
            .WithMany(s => s.Ingredients)
            .UsingEntity(j => j.ToTable("ingredient_category"));
    }
}
