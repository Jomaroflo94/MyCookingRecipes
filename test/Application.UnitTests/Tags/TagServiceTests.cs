using Domain.Ingredients;
using System.Xml.Linq;
using Domain.Shared;
using Domain.Tags;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Tags;
public class TagServiceTests
{
    private readonly ITagService _tagService;
    private readonly ITagRepository _tagRepository;

    public TagServiceTests()
    {        
        _tagRepository = Substitute.For<ITagRepository>();

        _tagService = new TagService(_tagRepository);
    }

    [Fact]
    public async Task GetTag_Should_ReturnGuid_WhenTagExist()
    {
        // Arrange
        Text Name = Text.Create("Tag").Value;
        var tag = Tag.Create(Guid.NewGuid(), Name, DateTime.UtcNow);

        _tagRepository.GetByNameAsync(Name, default)
            .Returns(tag);

        // Act
        Guid result = await _tagService.GetTagAsync(Name, default);

        // Assert
        result.Should().Be(tag.Id);
    }

    [Fact]
    public async Task GetTag_Should_ReturnNewGuid_WhenTagNotExist()
    {
        // Arrange
        Text Name = Text.Create("Tag").Value;

        _tagRepository.GetByNameAsync(Name, default)
            .Returns((Tag) null);

        // Act
        Guid result = await _tagService.GetTagAsync(Name, default);

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void GetTag_Should_ThrowArgumentException_WhenNameIsNull()
    {
        // Act
        Func<Task> func = async () => 
            await _tagService.GetTagAsync(null, default);

        // Assert
        func.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public void CreateTag_Should_ReturnGuid_WhenNameIsValid()
    {
        // Arrange
        Text Name = Text.Create("Tag").Value;

        // Act
        Guid result = _tagService.CreateTag(Name);

        // Assert
        _tagRepository.Received(1).Insert(Arg.Is<Tag>(u => u.Name == Name));
        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void CreateTag_Should_ThrowArgumentException_WhenNameIsNull()
    {
        // Act
        Action action = () => _tagService.CreateTag(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}
