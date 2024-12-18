using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class RecipeReadConfiguration : IEntityTypeConfiguration<RecipeRead>
{
    public void Configure(EntityTypeBuilder<RecipeRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.HasMany(r => r.RecipeIngredients)
              .WithOne(i => i.Recipe)
              .HasForeignKey(i => i.RecipeId);

        builder.HasMany(r => r.RecipeSteps)
              .WithOne(s => s.Recipe)
              .HasForeignKey(s => s.RecipeId);

        builder.HasMany(rt => rt.Tags)
            .WithMany(t => t.Recipes)
            .UsingEntity(j => j.ToTable("recipe_tag"));
    }
}
