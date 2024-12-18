using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class TagReadConfiguration : IEntityTypeConfiguration<TagRead>
{
    public void Configure(EntityTypeBuilder<TagRead> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.HasMany(rt => rt.Recipes)
            .WithMany(t => t.Tags)
            .UsingEntity(j => j.ToTable("recipe_tag"));
    }
}
