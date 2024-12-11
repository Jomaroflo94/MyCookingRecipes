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
    ITagRepository tagRepository) 
    : ICommandHandler<CreateIngredientCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
    {
        return await Result.Combine(
                Text.Create(command.Name)
                    .Ensure(async name => await IsNameUnique(name, cancellationToken), 
                        IngredientErrors.NameNotUnique),
                PDecimal.Create(command.Quantity).ToAsync())
            .Bind(async data => await CreateIngredient(data, cancellationToken));
    }

    private async Task<Result<Guid>> CreateIngredient((Text, PDecimal) data, CancellationToken cancellationToken)
    {
        var ingredient = Ingredient.Create(await GetTagId(data.Item1, cancellationToken),
            data.Item1, data.Item2, DateTime.UtcNow);

        ingredientRepository.Insert(ingredient);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ingredient.Id;
    }

    private async Task<bool> IsNameUnique(Text Name, CancellationToken cancellationToken)
    {
        return await ingredientRepository.IsNameUniqueAsync(Name, cancellationToken);
    }

    private async Task<Guid> GetTagId(Text Name, CancellationToken cancellationToken)
    {
        Tag? tag = await tagRepository.GetByNameAsync(Name, cancellationToken);

        return tag == null ? CreateTag(Name) : tag.Id;
    }

    private Guid CreateTag(Text Name)
    {
        var tag = Tag.Create(Guid.NewGuid(), Name, DateTime.UtcNow);

        tagRepository.Insert(tag);

        return tag.Id;
    }
}
