using Mediator.Domain;

namespace Domain.Recipes;

public sealed class RecipeIngredient : Entity
{
    #region Constructors
    private RecipeIngredient(Guid recipeId, Guid ingredientId, Guid unitId)
    {
        RecipeId = recipeId;
        IngredientId = ingredientId;
        UnitId = unitId;
    }

    private RecipeIngredient() { }
    #endregion

    #region Relation Properties
    public Guid RecipeId { get; private set; }
    public Guid IngredientId { get; private set; }
    public Guid UnitId { get; private set; }
    #endregion

    public static RecipeIngredient Create(Guid recipeId, Guid ingredientId, Guid unitId)
    {
        var recipeIngredient = new RecipeIngredient(recipeId, ingredientId, unitId);

        //ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipeIngredient;
    }
}
