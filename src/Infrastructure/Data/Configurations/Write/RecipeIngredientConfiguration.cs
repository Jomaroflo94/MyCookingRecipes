using Domain.Ingredients;
using Domain.Recipes;
using Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;
internal class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasKey(rt => new { rt.Id });

        builder.HasOne<Recipe>()
            .WithMany()
            .HasForeignKey(rt => rt.RecipeId);

        builder.HasOne<Ingredient>()
            .WithMany()
            .HasForeignKey(rt => rt.IngredientId);

        builder.HasOne<Unit>()
            .WithOne()
            .HasForeignKey<RecipeIngredient>(rt => rt.UnitId);
    }
}
