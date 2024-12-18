using Domain.Shared;
using Primitives.Entities;

namespace Domain.Categories;

public sealed class Category : Entity
{
    private Category(Ulid id, Text name,
        DateTime createdOnUtc) 
        : base(id, createdOnUtc) 
    {
        Name = name;
    }

    private Category() { }

    public Text Name { get; private set; }

    public static Category Create(Ulid id, Text name,
        DateTime createdOnUtc)
    {
        return new Category(id, name,
            createdOnUtc);
    }
}
