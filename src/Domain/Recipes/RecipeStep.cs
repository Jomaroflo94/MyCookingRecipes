using Domain.Shared;
using Mediator.Domain;

namespace Domain.Recipes;
public sealed class RecipeStep : Entity
{
    #region Constructors
    private RecipeStep(Guid recipeId, PInt order, Text description, 
        DateTime createdOnUtc)
    {
        RecipeId = recipeId;
        Order = order;
        Description = description;
        CreatedOnUtc = createdOnUtc;
    }

    private RecipeStep() { }
    #endregion

    #region Properties
    public PInt Order { get; private set; }
    public Text Description { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime UpdatedUtc { get; private set; }
    #endregion

    #region Relation Properties
    public Guid RecipeId { get; private set; }
    #endregion

    public static RecipeStep Create(Guid recipeId, PInt order, 
        Text description, DateTime createdOnUtc)
    {
        var recipeStep = new RecipeStep(recipeId, order, description, createdOnUtc);

        //recipeStep.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipeStep;
    }
}
