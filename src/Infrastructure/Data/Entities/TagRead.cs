namespace Infrastructure.Data.Entities;
internal class TagRead : EntityRead
{
    public required string Name { get; set; }

    // RelationsShips
    public IEnumerable<RecipeRead> Recipes { get; set; }
}
