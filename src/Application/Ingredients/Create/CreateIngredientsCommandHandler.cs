using Application.Abstractions.Data;
using Domain.Categories;
using Domain.Ingredients;
using Domain.Shared;
using Domain.Tags;
using Mediator.Application.Abstractions.Messaging;
using ROP;
using ROP.Extensions;

namespace Application.Ingredients.Create;

internal sealed class CreateIngredientsCommandHandler(IUnitOfWork unitOfWork,
    IIngredientRepository ingredientRepository,
    ICategoryService categoryService,
    ITagService tagService) 
    : ICommandHandler<CreateIngredientsCommand, IEnumerable<Ulid>>
{
    public async Task<Result<IEnumerable<Ulid>>> Handle(
        CreateIngredientsCommand command, 
        CancellationToken cancellationToken)
    {
        return await Result.Create(command.Ingredients)
            .Ensure(ValidateNames, IngredientErrors.NamesNotUnique, cancellationToken)
            .Bind(ValidateAndGetCategories, cancellationToken)
            .Bind(CreateIngredients(command.Ingredients), cancellationToken);
    }

    #region Private Methods

    private async Task<bool> ValidateNames(
        List<CreateIngredientRequest> ingredients,
        CancellationToken cancellationToken)
    {
        var names = ingredients.Select(s => s.Name).ToList();

        return await Result.Create(names.Select(Text.Create).ToList())
            .Ensure(ingredientRepository.AreNamesUniquesAsync,
                cancellationToken)
            .Match();
    }

    private async Task<Result<List<Category>>> ValidateAndGetCategories(
        List<CreateIngredientRequest> ingredients, 
        CancellationToken cancellationToken)
    {
        var categories = ingredients.Select(s => s.Categories).ToList();

        return await Result.Create(categories)
            .Ensure(e => e.Any(a => a.Count != 0), CategoryErrors.Empty)
            .Map(m => m.SelectMany(s => s).Distinct().ToList())
            .Bind(categoryService.ValidateAndGetCategories, cancellationToken);
    }

    private Func<List<Category>, CancellationToken, Task<Result<IEnumerable<Ulid>>>> 
        CreateIngredients(List<CreateIngredientRequest> list)
    {
        return async (categories, cancellationToken) =>
        {
            IEnumerable<(Ulid Id, Text Name)> tags = await tagService
                .GetTagsAsync(list.Select(s => Text.Create(s.Name)).ToList(),
                    cancellationToken);

            var ingredientsWithTags = list.Join(tags,
                ingredient => ingredient.Name,
                tag => tag.Name.Value,
                (ingredient, tag) => new { 
                    tag.Id, 
                    tag.Name, 
                    ingredient.Categories 
                });

            var finalIngredients = ingredientsWithTags.Select(ingredient =>
            {
                var associatedCategories = ingredient.Categories
                    .Join(categories,
                          categoryId => categoryId, 
                          category => category.Id,
                          (categoryId, category) => category)
                    .ToList();

                return new
                {
                    ingredient.Id,
                    ingredient.Name,
                    Categories = associatedCategories
                };
            });

            var ingredientEntities = finalIngredients
                .Select(s => Ingredient.Create(s.Id, s.Name, 
                    s.Categories, DateTime.UtcNow))
                .ToList();

            ingredientRepository.Insert(ingredientEntities);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ingredientEntities.Select(i => i.Id));
        };
    }

    #endregion
}
