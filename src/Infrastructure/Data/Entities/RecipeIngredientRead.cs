namespace Infrastructure.Data.Entities;
internal class RecipeIngredientRead
{
    public required Ulid RecipeId { get; set; }
    public RecipeRead Recipe { get; set; }

    public required Ulid IngredientId { get; set; }
    public IngredientRead Ingredient { get; set; }

    public required Ulid UnitId { get; set; }
    public UnitRead Unit { get; set; }
}
