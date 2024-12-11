using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class RecipeTagReadConfiguration : IEntityTypeConfiguration<RecipeTagRead>
{
    public void Configure(EntityTypeBuilder<RecipeTagRead> builder)
    {
        builder.HasKey(rt => new { rt.RecipeId, rt.TagId });

        builder.HasOne(rt => rt.Recipe)
            .WithMany(r => r.Tags)
            .HasForeignKey(rt => rt.RecipeId);

        builder.HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagId);
    }
}
