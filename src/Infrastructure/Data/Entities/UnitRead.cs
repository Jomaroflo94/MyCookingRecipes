namespace Infrastructure.Data.Entities;
internal class UnitRead : EntityRead
{
    public required string Name { get; set; }
    public required string Symbol { get; set; }

    // RelationsShips
    public IEnumerable<RecipeIngredientRead> RecipeIngredient { get; set; }
}
