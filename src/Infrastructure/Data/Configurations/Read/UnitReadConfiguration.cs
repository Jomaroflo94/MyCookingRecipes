using System.Reflection.Emit;
using Infrastructure.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read;
internal class UnitReadConfiguration : IEntityTypeConfiguration<UnitRead>
{
    public void Configure(EntityTypeBuilder<UnitRead> builder)
    {
        builder.HasKey(u => u.Id);
    }
}
