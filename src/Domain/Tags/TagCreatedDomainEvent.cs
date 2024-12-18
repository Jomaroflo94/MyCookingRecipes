using Mediator.Domain.Abstractions.Events;

namespace Domain.Tags;
public sealed record TagCreatedDomainEvent(Ulid TagId) : IDomainEvent;
