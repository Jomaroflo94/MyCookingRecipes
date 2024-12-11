using Domain.Recipes;
using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(Tag.Name))
                .HasMaxLength(50)
                .HasColumnName(nameof(Tag.Name).ToLower())
                .IsRequired());

        builder.Property(t => t.CreatedOnUtc).IsRequired();

        builder.HasMany<RecipeTag>()
            .WithOne()
            .HasForeignKey(rt => rt.TagId);
    }
}
