using Domain.Shared;
using Mediator.Domain;

namespace Domain.Tags;

public sealed class Tag : MediatorEntity
{
    private Tag(Ulid id, Text name, DateTime createdOnUtc)
        : base(id, createdOnUtc)
    {
        Name = name;
    }

    private Tag() { }

    public Text Name { get; private set; }

    public static Tag Create(Ulid id, Text name, DateTime createdOnUtc)
    {
        var tag = new Tag(id, name, createdOnUtc);

        tag.Raise(new TagCreatedDomainEvent(tag.Id));

        return tag;
    }
}
