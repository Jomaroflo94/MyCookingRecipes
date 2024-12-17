using Mediator.Application.Abstractions.Messaging;

namespace Application.Ingredients.Get;

public sealed record GetIngredientsQuery() : IQuery<IEnumerable<IngredientResponse>> { }
