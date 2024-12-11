using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class RecipeStepReadConfiguration : IEntityTypeConfiguration<RecipeStepRead>
{
    public void Configure(EntityTypeBuilder<RecipeStepRead> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(s => s.Recipe)
              .WithMany(r => r.Steps)
              .HasForeignKey(s => s.RecipeId);
    }
}
