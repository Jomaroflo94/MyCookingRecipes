using ROP;

namespace Domain.Ingredients;

public static class IngredientErrors
{
    public static Error NotFound(Ulid ingredientId) => Error.NotFound(
        "Ingredients.NotFound", 
        $"The ingredient with the Id = '{ingredientId}' was not found");

    public static readonly Error NoneFound = Error.NoContent(
        "Ingredients.NoneFound", $"No existing ingredients");

    public static readonly Error NameNotUnique = Error.Conflict(
        "Ingredients.NameNotUnique", "The provided name is not unique");

    public static readonly Error NamesNotUnique = Error.Conflict(
        "Ingredients.NamesNotUnique", 
        "One or more of the specified name already exist");
}
