namespace Infrastructure.Data.Entities;
internal class TagRead
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedUtc { get; set; }

    // Relationships
    public IEnumerable<RecipeTagRead> RecipeTags { get; set; }
}
