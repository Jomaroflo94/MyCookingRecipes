namespace Application.Ingredients.Create;

public sealed record CreateIngredientRequest(string Name, List<Ulid> Categories);
