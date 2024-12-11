using Mediator.Domain.Abstractions.Events;

namespace Domain.Ingredients;

public sealed record IngredientCreatedDomainEvent(Guid IngredientId) : IDomainEvent;
