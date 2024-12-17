using Mediator.Application.Abstractions.Messaging;

namespace Application.Ingredients.Create;
public sealed record CreateIngredientsCommand(List<CreateIngredientRequest> Ingredients)
    : ICommand;
