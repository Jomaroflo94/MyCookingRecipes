namespace Infrastructure.Outbox;
internal sealed record OutboxMessage(Ulid Id, string Name, string Content,
    DateTime CreatedOnUtc, DateTime? ProcessedOnUtc = null, string? Error = null);
