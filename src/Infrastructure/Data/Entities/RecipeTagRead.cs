namespace Infrastructure.Data.Entities;
internal class RecipeTagRead
{
    public required Guid RecipeId { get; set; }
    public RecipeRead Recipe { get; set; }

    public required Guid TagId { get; set; }
    public TagRead Tag { get; set; }
}
