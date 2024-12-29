using Domain.Categories;
using Domain.Ingredients;
using Domain.Shared;
using FluentAssertions;
using Primitives.Exceptions;
using Xunit;

#nullable disable

namespace Domain.UnitTests.Ingredients;

public class IngredientTests
{
    private static readonly Ulid Id = Ulid.NewUlid();
    private static readonly Text Name = new("Ingredient");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    private static readonly List<Category> Categories = [
        new (Id, new Text("Category"), UtcNow)
    ];

    [Fact]
    public void Create_Should_Throw_ArgumentNullException_WhenCategoriesIsNull()
    {
        // Act
        Action action = () => Ingredient.Create(Id, Name, null, UtcNow);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_Should_Throw_EmptyListException_WhenCategoriesIsEmpty()
    {
        // Act
        Action action = () => Ingredient.Create(Id, Name, [], UtcNow);

        // Assert
        action.Should().Throw<EmptyListException>();
    }

    [Fact]
    public void Create_Should_CreateIngredient_WhenAllAreValid()
    {
        // Act
        var ingredient = Ingredient.Create(Id, Name, Categories, UtcNow);

        // Assert
        ingredient.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenAllAreValid()
    {
        // Act
        var ingredient = Ingredient.Create(Id, Name, Categories, UtcNow);

        // Assert
        ingredient.DomainEvents
            .Should().ContainSingle()
            .Which
            .Should().BeOfType<IngredientCreatedDomainEvent>();
    }
}
