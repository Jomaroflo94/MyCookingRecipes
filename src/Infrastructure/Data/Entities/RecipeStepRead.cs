namespace Infrastructure.Data.Entities;
internal class RecipeStepRead : EntityRead
{
    public required int Order { get; set; }
    public required string Description { get; set; }

    //FK
    public Ulid RecipeId { get; set; }

    // Relationships
    public RecipeRead Recipe { get; set; }
}
