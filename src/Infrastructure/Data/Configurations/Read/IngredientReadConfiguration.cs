using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;

internal sealed class IngredientReadConfiguration : IEntityTypeConfiguration<IngredientRead>
{
    public void Configure(EntityTypeBuilder<IngredientRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(s => s.Tag)
              .WithOne()
              .HasForeignKey<IngredientRead>(s => s.TagId);

        builder.HasMany(t => t.RecipeIngredients)
            .WithOne(rt => rt.Ingredient)
            .HasForeignKey(rt => rt.IngredientId);
    }
}
