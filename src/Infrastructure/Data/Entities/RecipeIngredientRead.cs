namespace Infrastructure.Data.Entities;
internal class RecipeIngredientRead
{
    public required Guid RecipeId { get; set; }
    public RecipeRead Recipe { get; set; }

    public required Guid IngredientId { get; set; }
    public IngredientRead Ingredient { get; set; }

    public required Guid UnitId { get; set; }
    public UnitRead Unit { get; set; }
}
