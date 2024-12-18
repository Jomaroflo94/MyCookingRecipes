namespace Domain.Recipes;

public sealed class RecipeIngredient
{
    public Ulid RecipeId { get; private set; }
    public Ulid IngredientId { get; private set; }
    public Ulid UnitId { get; private set; }
}
