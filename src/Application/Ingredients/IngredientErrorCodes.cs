namespace Application.Ingredients;
public static class IngredientErrorCodes
{
    public static class CreateIngredient
    {
        public const string MissingName = nameof(MissingName);
        public const string NegativeOrZeroQuantity = nameof(NegativeOrZeroQuantity);
    }
}
