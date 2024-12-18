using Mediator.Application.Abstractions.Messaging;

namespace Application.Ingredients.Create;

public sealed record CreateIngredientCommand(string Name, decimal Quantity)
    : ICommand<Ulid>;
