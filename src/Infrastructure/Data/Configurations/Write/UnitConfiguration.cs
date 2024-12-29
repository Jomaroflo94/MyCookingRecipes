using Domain.Recipes;
using Domain.Shared;
using Domain.Tags;
using Domain.Units;
using Infrastructure.Data.Converters;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Unit.Name))
                .HasMaxLength(50)
                .HasColumnName(nameof(Unit.Name).ToLower())
                .IsRequired());

        builder.ComplexProperty(
            u => u.Symbol,
            b => b.Property(e => e.Value).HasColumnName(nameof(Unit.Symbol))
                .HasMaxLength(10)
                .HasColumnName(nameof(Unit.Symbol).ToLower())
                .IsRequired());

        builder.HasMany<RecipeIngredient>()
            .WithOne()
            .HasForeignKey(rt => rt.UnitId);
    }
}
