using Mediator.Domain.Abstractions.Events;

namespace Domain.Tags;
public sealed record TagCreatedDomainEvent(Guid TagId) : IDomainEvent;
