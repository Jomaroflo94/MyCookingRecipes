using Domain.Shared;
using Mediator.Domain;
using Primitives;
using ROP;

namespace Domain.Tags;

public sealed class Tag : Entity
{
    #region Constructors
    private Tag(Guid id, Text name, DateTime createdOnUtc)
        : base(id)
    {
        Name = name;
        CreatedOnUtc = createdOnUtc;
    }

    private Tag() { }
    #endregion

    #region Properties
    public Text Name { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime UpdatedUtc { get; private set; }
    #endregion

    public static Tag Create(Guid id, Text name, DateTime createdOnUtc)
    {
        var tag = new Tag(id, name, createdOnUtc);

        tag.Raise(new TagCreatedDomainEvent(tag.Id));

        return tag;
    }
}
