using Mediator.Application.Abstractions.Messaging;

namespace Application.Ingredients.Get;

public sealed record GetIngredientByIdQuery(Ulid IngredientId) : IQuery<IngredientResponse> { }
