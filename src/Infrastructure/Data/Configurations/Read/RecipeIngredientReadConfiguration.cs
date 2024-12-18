using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class RecipeIngredientReadConfiguration : IEntityTypeConfiguration<RecipeIngredientRead>
{
    public void Configure(EntityTypeBuilder<RecipeIngredientRead> builder)
    {
        builder.HasKey(rt => new { rt.RecipeId, rt.IngredientId, rt.UnitId });

        builder.Property(p => p.RecipeId)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.Property(p => p.IngredientId)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.Property(p => p.UnitId)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(rt => rt.RecipeId);

        builder.HasOne(rt => rt.Ingredient)
            .WithMany(t => t.RecipeIngredients)
            .HasForeignKey(rt => rt.IngredientId);

        builder.HasOne(rt => rt.Unit)
            .WithOne(t => t.RecipeIngredient)
            .HasForeignKey<RecipeIngredientRead>(rt => rt.UnitId);
    }
}
