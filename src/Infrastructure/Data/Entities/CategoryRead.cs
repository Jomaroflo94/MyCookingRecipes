namespace Infrastructure.Data.Entities;

internal class CategoryRead
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    //RelationsShips
    public IEnumerable<IngredientRead> Ingredients { get; set; }
}

