using Domain.Shared;
using Mediator.Domain;

namespace Domain.Recipes;
public sealed class RecipeStep : MediatorEntity
{
    private RecipeStep(Ulid id, Ulid recipeId, PInt order, Text description, 
        DateTime createdOnUtc) : base(id, createdOnUtc)
    {
        RecipeId = recipeId;
        Order = order;
        Description = description;
    }

    private RecipeStep() { }

    public PInt Order { get; private set; }
    public Text Description { get; private set; }
    public Ulid RecipeId { get; private set; }

    public static RecipeStep Create(Ulid id, Ulid recipeId, PInt order, 
        Text description, DateTime createdOnUtc)
    {
        var recipeStep = new RecipeStep(id, recipeId, order, description, 
            createdOnUtc);

        //recipeStep.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipeStep;
    }
}
