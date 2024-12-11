namespace Infrastructure.Data.Entities;
internal class RecipeStepRead
{
    public required Guid Id { get; set; }
    public required int Order { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedUtc { get; set; }

    //FK
    public Guid RecipeId { get; set; }

    // Relationships
    public RecipeRead Recipe { get; set; }
}
