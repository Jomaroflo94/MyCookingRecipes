namespace Infrastructure.Data.Entities;

internal class IngredientRead : EntityRead
{
    public required string Name { get; set; }

    // Foreign Keys
    public Ulid TagId { get; set; }

    // Relationships
    public TagRead Tag { get; set; }
    public IEnumerable<CategoryRead> Categories { get; set; }
    public IEnumerable<RecipeIngredientRead> RecipeIngredients { get; set; }
}
