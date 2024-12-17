using Application.Abstractions.Data;
using Domain.Ingredients;
using Domain.Shared;
using Domain.Tags;
using Mediator.Application.Abstractions.Messaging;
using ROP;
using ROP.Extensions;

namespace Application.Ingredients.Create;

internal sealed class CreateIngredientsCommandHandler(IUnitOfWork unitOfWork,
    IIngredientRepository ingredientRepository,
    ITagService tagService) 
    : ICommandHandler<CreateIngredientsCommand>
{
    public async Task<Result> Handle(CreateIngredientsCommand command, 
        CancellationToken cancellationToken)
    {
        return await Result.Combine(command.Ingredients
            .Select(async s => 
                await ValidateFields(s.Name, s.Quantity)).ToArray())
            .Ensure(async e => 
                await AreNamesUniques(e.Select(n => n.Item1).ToList(), cancellationToken),
                    IngredientErrors.NameNotUnique)
            .Bind(b => CreateIngredients(b, cancellationToken));
    }

    private static async Task<Result<(Text, PDecimal)>> ValidateFields(
        string Name, decimal Quantity)
    {
        return await Result.Combine(
            Text.Create(Name).ToAsync(),
            PDecimal.Create(Quantity).ToAsync());
    }

    private async Task<bool> AreNamesUniques(List<Text> Names, 
        CancellationToken cancellationToken)
    {
        return await ingredientRepository
            .AreNamesUniquesAsync(Names, cancellationToken);
    }

    private async Task<Result> CreateIngredients(
        (Text, PDecimal)[] data, 
        CancellationToken cancellationToken)
    {
        IEnumerable<(Guid Id, Text Name)> tags = await tagService.GetTagsAsync(
            data.Select(s => s.Item1).ToList(), 
            cancellationToken);

        IEnumerable<Ingredient> ingredients = data
            .Join(tags, data => data.Item1, tag => tag.Name,
                (data, tag) => new { tag.Id, data.Item1, data.Item2 })
            .Select(s => Ingredient.Create(s.Id, s.Item1, s.Item2, 
                DateTime.UtcNow));

        ingredientRepository.Insert(ingredients.ToList());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
