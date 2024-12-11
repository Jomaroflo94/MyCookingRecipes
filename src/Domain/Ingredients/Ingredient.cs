using Domain.Shared;
using Mediator.Domain;

namespace Domain.Ingredients;

public sealed class Ingredient : Entity
{
    #region Constructors
    private Ingredient(Guid id, Text name, 
        PDecimal quantity, DateTime createdOnUtc)
        : base(id)
    {
        TagId = id;
        Name = name;
        Quantity = quantity;
        CreatedOnUtc = createdOnUtc;
    }

    private Ingredient() { }
    #endregion

    #region Properties
    public Text Name { get; private set; }
    public PDecimal Quantity { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime UpdatedUtc { get; private set; }
    #endregion

    #region Relation Properties
    public Guid TagId { get; private set; }
    #endregion

    public static Ingredient Create(Guid id, Text name, 
        PDecimal quantity, DateTime createdOnUtc)
    {
        var ingredient = new Ingredient(id, name, 
            quantity, createdOnUtc);

        ingredient.Raise(new IngredientCreatedDomainEvent(ingredient.Id));

        return ingredient;
    }
}
