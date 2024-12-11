using Domain.Ingredients;
using Domain.Shared;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Ingredients;

public class IngredientTests
{
    private static readonly Guid Id = Guid.NewGuid();
    private static readonly Text Name = new("Ingredient");
    private static readonly PDecimal Quantity = new(1);
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    [Fact]
    public void Create_Should_CreateTag_WhenAllAreValid()
    {
        // Act
        var ingredient = Ingredient.Create(Id, Name, Quantity, UtcNow);

        // Assert
        ingredient.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenAllAreValid()
    {
        // Act
        var ingredient = Ingredient.Create(Id, Name, Quantity, UtcNow);

        // Assert
        ingredient.DomainEvents
            .Should().ContainSingle()
            .Which
            .Should().BeOfType<IngredientCreatedDomainEvent>();
    }
}
