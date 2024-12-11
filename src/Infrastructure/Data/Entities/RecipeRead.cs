namespace Infrastructure.Data.Entities;
internal class RecipeRead
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedUtc { get; set; }

    // Relationship
    public IEnumerable<RecipeIngredientRead> Ingredients { get; set; }
    public IEnumerable<RecipeStepRead> Steps { get; set; }
    public IEnumerable<RecipeTagRead> Tags { get; set; }
}
