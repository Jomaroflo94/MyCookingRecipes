using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class TagReadConfiguration : IEntityTypeConfiguration<TagRead>
{
    public void Configure(EntityTypeBuilder<TagRead> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.RecipeTags)
            .WithOne(rt => rt.Tag)
            .HasForeignKey(rt => rt.TagId);
    }
}
