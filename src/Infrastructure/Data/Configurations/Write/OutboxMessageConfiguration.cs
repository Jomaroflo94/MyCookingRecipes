using Infrastructure.Data.Converters;
using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion<UlidToStringConverter>();

        builder.ToTable("OutboxMessages");
    }
}
