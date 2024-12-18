namespace Infrastructure.Data.Entities;
internal class EntityRead
{
    public required Ulid Id { get; set; }
    public required DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedUtc { get; set; }
    public DateTime? DeletedUtc { get; set; }
}
