using Domain.Categories;
using Domain.Shared;
using Mediator.Domain;
using Primitives.Guards;

namespace Domain.Ingredients;

public sealed class Ingredient : MediatorEntity
{
    private Ingredient(Ulid id, Text name, 
        List<Category> categories, DateTime createdOnUtc)
        : base(id, createdOnUtc)
    {
        TagId = id;
        Name = name;
        Categories = categories;
    }

    private Ingredient() { }

    public Text Name { get; private set; }
    public Ulid TagId { get; private set; }

    public List<Category> Categories { get; set; }

    public static Ingredient Create(Ulid id, Text name, 
        List<Category> categories, DateTime createdOnUtc)
    {
        Ensure.NotNullOrEmpty(categories);

        var ingredient = new Ingredient(id, name,
            categories, createdOnUtc);

        //TODO: Revisar
        //ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return ingredient;
    }
}
