using Mediator.Domain;

namespace Domain.Ingredients;
public sealed class IngredientCategory : Entity
{
    #region Constructors
    private IngredientCategory(Guid ingredientId, Guid categoryId)
    {
        IngredientId = ingredientId;
        CategoryId = categoryId;
    }

    private IngredientCategory() { }
    #endregion

    #region Relation Properties
    public Guid IngredientId { get; private set; }
    public Guid CategoryId { get; private set; }
    #endregion

    public static IngredientCategory Create(Guid ingredientId, Guid categoryId)
    {
        var ingredientCategory = new IngredientCategory(ingredientId, categoryId);

        //ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return ingredientCategory;
    }
}
