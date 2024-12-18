namespace Application.Ingredients.Get;

public sealed record IngredientResponse
{
    public Ulid Id { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
}
