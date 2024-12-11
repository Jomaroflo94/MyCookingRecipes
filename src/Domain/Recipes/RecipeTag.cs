using Mediator.Domain;

namespace Domain.Recipes;

public sealed class RecipeTag : Entity
{
    #region Constructors
    private RecipeTag(Guid recipeId, Guid tagId) 
    {
        RecipeId = recipeId;
        TagId = tagId;
    }

    private RecipeTag() { }
    #endregion

    #region Relation Properties
    public Guid RecipeId { get; private set; }
    public Guid TagId { get; private set; }
    #endregion

    public static RecipeTag Create(Guid recipeId, Guid tagId)
    {
        var recipeTag = new RecipeTag(recipeId, tagId);

        //ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipeTag;
    }
}
