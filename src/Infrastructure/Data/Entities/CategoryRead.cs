namespace Infrastructure.Data.Entities;

internal class CategoryRead : EntityRead
{
    public required string Name { get; set; }

    // Relationships
    public IEnumerable<IngredientRead> Ingredients { get; set; }
}
