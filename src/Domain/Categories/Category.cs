using Domain.Ingredients;
using Domain.Shared;
using Primitives.Entities;

namespace Domain.Categories;

public sealed class Category : Entity
{
    public Category(Ulid id, Text name,
        DateTime createdOnUtc) 
        : base(id, createdOnUtc) 
    {
        Name = name;
    }

    private Category() { }

    public Text Name { get; private set; }

    public List<Ingredient> Ingredients { get; set; }
}
