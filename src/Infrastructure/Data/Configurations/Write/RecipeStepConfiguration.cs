using Domain.Recipes;
using Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.ComplexProperty(
            u => u.Order,
            b => b.Property(e => e.Value).HasColumnName(nameof(RecipeStep.Order))
                .HasColumnName(nameof(RecipeStep.Order).ToLower())
                .IsRequired());

        builder.ComplexProperty(
            u => u.Description,
            b => b.Property(e => e.Value).HasColumnName(nameof(RecipeStep.Description))
                .HasMaxLength(1000)
                .HasColumnName(nameof(RecipeStep.Description).ToLower())
                .IsRequired());

        builder.HasOne<Recipe>()
              .WithMany()
              .HasForeignKey(s => s.RecipeId);
    }
}
