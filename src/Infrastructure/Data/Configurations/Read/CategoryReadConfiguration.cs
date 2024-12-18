using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class CategoryReadConfiguration : IEntityTypeConfiguration<CategoryRead>
{
    public void Configure(EntityTypeBuilder<CategoryRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.HasMany(t => t.Ingredients)
            .WithMany(s => s.Categories)
            .UsingEntity(j => j.ToTable("ingredient_category"));
    }
}
