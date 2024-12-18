using Application.Abstractions.Data;
using Domain.Ingredients;
using Domain.Shared;
using Domain.Tags;
using Mediator.Application.Abstractions.Messaging;
using ROP;
using ROP.Extensions;

namespace Application.Ingredients.Create;

internal sealed class CreateIngredientCommandHandler(IUnitOfWork unitOfWork,
    IIngredientRepository ingredientRepository,
    ITagService tagService) 
    : ICommandHandler<CreateIngredientCommand, Ulid>
{
    public async Task<Result<Ulid>> Handle(CreateIngredientCommand command, 
        CancellationToken cancellationToken)
    {
        return await Result.Combine(
                Text.Create(command.Name)
                    .Ensure(async name => await IsNameUnique(name, cancellationToken), 
                        IngredientErrors.NameNotUnique),
                PDecimal.Create(command.Quantity).ToAsync())
            .Bind(async data => await CreateIngredient(data, cancellationToken));
    }

    private async Task<Result<Ulid>> CreateIngredient((Text, PDecimal) data, 
        CancellationToken cancellationToken)
    {
        var ingredient = Ingredient.Create(await tagService.GetTagAsync(
                data.Item1, cancellationToken),
            data.Item1, data.Item2, DateTime.UtcNow);

        ingredientRepository.Insert(ingredient);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ingredient.Id;
    }

    private async Task<bool> IsNameUnique(Text Name, 
        CancellationToken cancellationToken)
    {
        return await ingredientRepository
            .IsNameUniqueAsync(Name, cancellationToken);
    }
}
