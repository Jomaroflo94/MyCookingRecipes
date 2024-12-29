using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class UnitReadConfiguration : IEntityTypeConfiguration<UnitRead>
{
    public void Configure(EntityTypeBuilder<UnitRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.HasMany(s => s.RecipeIngredient)
            .WithOne(r => r.Unit)
            .HasForeignKey(s => s.UnitId);
    }
}
