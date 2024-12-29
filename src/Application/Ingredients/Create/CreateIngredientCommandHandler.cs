using Application.Abstractions.Data;
using Domain.Categories;
using Domain.Ingredients;
using Domain.Shared;
using Domain.Tags;
using Mediator.Application.Abstractions.Messaging;
using ROP;
using ROP.Extensions;

namespace Application.Ingredients.Create;

internal sealed class CreateIngredientCommandHandler(IUnitOfWork unitOfWork,
    IIngredientRepository ingredientRepository,
    ICategoryService categoryService,
    ITagService tagService)
    : ICommandHandler<CreateIngredientCommand, Ulid>
{
    public async Task<Result<Ulid>> Handle(
        CreateIngredientCommand command,
        CancellationToken cancellationToken)
    {
        return await Result.Combine(
                ValidateName(command.Name, cancellationToken),
                ValidateAndGetCategories(command.Categories, cancellationToken))
            .Bind(CreateIngredient, cancellationToken);
    }

    #region Private Methods

    private async Task<Result<Text>> ValidateName(
        string name,
        CancellationToken cancellationToken)
    {
        return await Result.Create(Text.Create(name))
            .Ensure(ingredientRepository.IsNameUniqueAsync,
                IngredientErrors.NameNotUnique,
                cancellationToken);
    }

    private async Task<Result<List<Category>>> ValidateAndGetCategories(
        List<Ulid> categories, 
        CancellationToken cancellationToken)
    {
        return await categoryService
            .ValidateAndGetCategories(categories, cancellationToken);
    }

    private async Task<Result<Ulid>> CreateIngredient(
        (Text, List<Category>) data,
        CancellationToken cancellationToken)
    {
        Ulid tagId = await tagService.GetTagAsync(data.Item1, cancellationToken);

        var ingredient = Ingredient.Create(tagId, data.Item1, 
            data.Item2, DateTime.UtcNow);

        ingredientRepository.Insert(ingredient);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ingredient.Id;
    }

    #endregion
}
