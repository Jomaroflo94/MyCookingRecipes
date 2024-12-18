using Domain.Shared;
using Mediator.Domain;

namespace Domain.Ingredients;

public sealed class Ingredient : MediatorEntity
{
    private Ingredient(Ulid id, Text name, 
        PDecimal quantity, DateTime createdOnUtc)
        : base(id, createdOnUtc)
    {
        TagId = id;
        Name = name;
        Quantity = quantity;
    }

    private Ingredient() { }

    public Text Name { get; private set; }
    public PDecimal Quantity { get; private set; }
    public Ulid TagId { get; private set; }

    public static Ingredient Create(Ulid id, Text name, 
        PDecimal quantity, DateTime createdOnUtc)
    {
        var ingredient = new Ingredient(id, name, 
            quantity, createdOnUtc);

        //TODO: Revisar
        //ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return ingredient;
    }
}
