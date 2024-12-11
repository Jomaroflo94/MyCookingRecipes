using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class RecipeReadConfiguration : IEntityTypeConfiguration<RecipeRead>
{
    public void Configure(EntityTypeBuilder<RecipeRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(r => r.Ingredients)
              .WithOne(i => i.Recipe)
              .HasForeignKey(i => i.RecipeId);

        builder.HasMany(r => r.Steps)
              .WithOne(s => s.Recipe)
              .HasForeignKey(s => s.RecipeId);

        builder.HasMany(r => r.Tags)
              .WithOne(rt => rt.Recipe)
              .HasForeignKey(rt => rt.RecipeId);
    }
}
