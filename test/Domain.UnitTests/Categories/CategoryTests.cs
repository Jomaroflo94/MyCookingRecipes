using Domain.Categories;
using Domain.Shared;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Categories;
public class CategoryTests
{
    private static readonly Ulid Id = Ulid.NewUlid();
    private static readonly Text Name = new("Category");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    [Fact]
    public void Create_Should_CreateCategory_WhenAllAreValid()
    {
        // Act
        var category = new Category(Id, Name, UtcNow);

        // Assert
        category.Should().NotBeNull();
        category.Id.Should().NotBeNull();
        category.Name.Should().NotBeNull()
            .And.Subject.Should().BeOfType<Text>()
                .Which.Value.Should().Be("Category");
        ;
    }
}
