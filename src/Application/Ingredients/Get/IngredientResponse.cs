namespace Application.Ingredients.Get;

public sealed record IngredientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
}
