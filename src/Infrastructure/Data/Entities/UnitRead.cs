namespace Infrastructure.Data.Entities;
internal class UnitRead
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }

    //RelationsShips
    public RecipeIngredientRead RecipeIngredient { get; set; }
}
