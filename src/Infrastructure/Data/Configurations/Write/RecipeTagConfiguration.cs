using Domain.Recipes;
using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
{
    public void Configure(EntityTypeBuilder<RecipeTag> builder)
    {
        builder.HasKey(rt => new { rt.Id });

        builder.HasOne<Recipe>()
            .WithMany()
            .HasForeignKey(rt => rt.RecipeId);

        builder.HasOne<Tag>()
            .WithMany()
            .HasForeignKey(rt => rt.TagId);
    }
}
