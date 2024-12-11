using Domain.Shared;
using Mediator.Domain;

namespace Domain.Recipes;
public sealed class Recipe : Entity
{
    #region Constructors
    private Recipe(Text name, Text description,
        DateTime createdOnUtc)
    {
        Name = name;
        Description = description;
        CreatedOnUtc = createdOnUtc;
    }

    private Recipe() { }
    #endregion

    #region Properties
    public Text Name { get; private set; }
    public Text Description { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime UpdatedUtc { get; private set; }
    #endregion

    public static Recipe Create(Text name, Text description,
        DateTime createdOnUtc)
    {
        var recipe = new Recipe(name, description, createdOnUtc);

        //recipe.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipe;
    }
}
