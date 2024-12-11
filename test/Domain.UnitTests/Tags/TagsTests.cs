using Domain.Shared;
using Domain.Tags;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Tags;
public class TagsTests
{
    private static readonly Guid Id = Guid.NewGuid();
    private static readonly Text Name = new("Tag");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    [Fact]
    public void Create_Should_CreateTag_WhenAllAreValid()
    {
        // Act
        var tag = Tag.Create(Id, Name, UtcNow);

        // Assert
        tag.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenAllAreValid()
    {
        // Act
        var tag = Tag.Create(Id, Name, UtcNow);

        // Assert
        tag.DomainEvents
            .Should().ContainSingle()
            .Which
            .Should().BeOfType<TagCreatedDomainEvent>();
    }
}
