namespace Infrastructure.Data.Entities;
internal class RecipeRead : EntityRead
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    // Relationship
    public IEnumerable<RecipeIngredientRead> RecipeIngredients { get; set; }
    public IEnumerable<RecipeStepRead> RecipeSteps { get; set; }
    public IEnumerable<TagRead> Tags { get; set; }
}
