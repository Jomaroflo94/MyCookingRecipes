using Domain.Shared;
using Mediator.Domain;

namespace Domain.Recipes;
public sealed class Recipe : MediatorEntity
{
    private Recipe(Ulid id, Text name, Text description,
        DateTime createdOnUtc) : base(id, createdOnUtc)
    {
        Name = name;
        Description = description;
    }

    private Recipe() { }

    public Text Name { get; private set; }
    public Text Description { get; private set; }

    public static Recipe Create(Ulid id, Text name, 
        Text description, DateTime createdOnUtc)
    {
        var recipe = new Recipe(id, name, description, createdOnUtc);

        //recipe.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return recipe;
    }
}
