using FluentValidation;

namespace Application.Ingredients.Create;
internal class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(IngredientErrorCodes.CreateIngredient.MissingName);

        RuleFor(c => c.Quantity)
            .GreaterThan(0).WithErrorCode(IngredientErrorCodes.CreateIngredient.NegativeOrZeroQuantity);
    }
}
