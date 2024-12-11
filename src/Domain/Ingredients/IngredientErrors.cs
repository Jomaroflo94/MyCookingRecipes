using ROP;

namespace Domain.Ingredients;

public static class IngredientErrors
{
    public static Error NotFound(Guid ingredientId) => Error.NotFound(
        "Ingredients.NotFound", $"The ingredient with the Id = '{ingredientId}' was not found");

    public static readonly Error NameNotUnique = Error.Conflict(
        "Ingredients.NameNotUnique", "The provided name is not unique");
}
