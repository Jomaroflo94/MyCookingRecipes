namespace Infrastructure.Data.Entities;

internal class IngredientRead
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Quantity { get; set; }
    public required DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedUtc { get; set; }

    //FK
    public Guid TagId { get; set; }


    // Relationships
    public TagRead Tag { get; set; }
    public IEnumerable<RecipeIngredientRead> RecipeIngredients { get; set; }
}
