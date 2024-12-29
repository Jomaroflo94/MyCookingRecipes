using FluentValidation;

namespace Application.Ingredients.Create;
internal class CreateIngredientsCommandValidator : AbstractValidator<CreateIngredientsCommand>
{
    public CreateIngredientsCommandValidator()
    {
        RuleFor(c => c.Ingredients)
            .NotEmpty()
                .WithErrorCode(IngredientErrorCodes.CreateIngredient.Empty)
            .Must(ingredients => ingredients.GroupBy(i => i.Name).All(g => g.Count() == 1))
                .WithErrorCode(IngredientErrorCodes.CreateIngredient.DuplicateName)
                .WithMessage("Ingredients must have unique names.")
            .ForEach(ingredient => ingredient.NotNull()
                    .WithErrorCode(IngredientErrorCodes.CreateIngredient.Null)
                .ChildRules(child =>
                {
                    child.RuleFor(i => i.Name)
                        .NotEmpty().WithErrorCode(IngredientErrorCodes.CreateIngredient.MissingName);

                    child.RuleFor(i => i.Categories)
                        .NotEmpty().WithErrorCode(IngredientErrorCodes.CreateIngredient.MissingCategories);
                })
            );
    }
}
