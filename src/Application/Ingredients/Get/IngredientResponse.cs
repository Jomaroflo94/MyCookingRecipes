using Application.Shared.Get;

namespace Application.Ingredients.Get;

public sealed record IngredientResponse : EntityResponse
{
    public IEnumerable<EntityResponse> Categories { get; set; }
}
