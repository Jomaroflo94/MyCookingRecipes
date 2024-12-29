using Domain.Shared;
using Domain.Tags;
using FluentAssertions;
using NSubstitute;
using Xunit;

#nullable disable

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

    #region GetTag

    [Fact]
    public async Task GetTag_Should_ReturnUlid_WhenTagExist()
    {
        // Arrange
        var Name = Text.Create("Tag");
        var tag = Tag.Create(Ulid.NewUlid(), Name, DateTime.UtcNow);

        _tagRepository.GetByNameAsync(Name, default)
            .Returns(tag);

        // Act
        Ulid result = await _tagService.GetTagAsync(Name, default);

        // Assert
        result.Should().Be(tag.Id);
    }

    [Fact]
    public async Task GetTag_Should_ReturnNewUlid_WhenTagNotExist()
    {
        // Arrange
        var Name = Text.Create("Tag");

        _tagRepository.GetByNameAsync(Name, default)
            .Returns((Tag) null);

        // Act
        Ulid result = await _tagService.GetTagAsync(Name, default);

        // Assert
        result.Should().NotBe(Ulid.Empty);
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

    #endregion

    #region GetTags

    [Fact]
    public async Task GetTags_Should_ReturnUlids_WhenAllTagsExist()
    {
        // Arrange
        var Name1 = Text.Create("Tag1");
        var Name2 = Text.Create("Tag2");

        var tag1 = Tag.Create(Ulid.NewUlid(), Name1, DateTime.UtcNow);
        var tag2 = Tag.Create(Ulid.NewUlid(), Name2, DateTime.UtcNow);

        _tagRepository.GetByNamesAsync(Arg.Any<List<Text>>(), 
                Arg.Any<CancellationToken>())
            .Returns([tag1, tag2]);

        // Act
        IEnumerable<(Ulid Id, Text Name)> result = await _tagService
            .GetTagsAsync([Name1, Name2], default);

        // Assert
        result.Should().NotBeEmpty().And
            .BeEquivalentTo([(tag1.Id, tag1.Name), (tag2.Id, tag2.Name)], 
                options => options.WithStrictOrdering());
    }

    [Fact]
    public async Task GetTags_Should_ReturnNewUlids_WhenNoneTagExist()
    {
        // Arrange
        var Name1 = Text.Create("Tag1");
        var Name2 = Text.Create("Tag2");

        _tagRepository.GetByNamesAsync(Arg.Any<List<Text>>(),
                Arg.Any<CancellationToken>())
            .Returns((List<Tag>) null);

        // Act
        IEnumerable<(Ulid Id, Text Name)> result = await _tagService
            .GetTagsAsync([Name1, Name2], default);

        // Assert
        result.Should().NotBeEmpty().And.HaveCount(2);
    }

    [Fact]
    public async Task GetTags_Should_ReturnUlids_WhenSomeTagExist()
    {
        // Arrange
        var Name1 = Text.Create("Tag1");
        var Name2 = Text.Create("Tag2");

        var tag1 = Tag.Create(Ulid.NewUlid(), Name1, DateTime.UtcNow);

        _tagRepository.GetByNamesAsync(Arg.Any<List<Text>>(),
                Arg.Any<CancellationToken>())
            .Returns([tag1]);

        // Act
        IEnumerable<(Ulid Id, Text Name)> result = await _tagService
            .GetTagsAsync([Name1, Name2], default);

        // Assert
        result.Should().NotBeEmpty().And.HaveCount(2).And
            .Contain([(tag1.Id, tag1.Name)]);
    }

    #endregion

    #region CreateTag

    [Fact]
    public void CreateTag_Should_ReturnUlid_WhenNameIsValid()
    {
        // Arrange
        var Name = Text.Create("Tag");

        // Act
        Ulid result = _tagService.CreateTag(Name);
        
        // Assert
        result.Should().NotBe(Ulid.Empty);
    }

    [Fact]
    public void CreateTag_Should_ThrowArgumentException_WhenNameIsNull()
    {
        // Act
        Action action = () => _tagService.CreateTag(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region CreateTags

    //[Fact]
    //public void CreateTags_Should_ReturnUlids_WhenAllNameAreValid()
    //{
    //    // Arrange
    //    var Name1 = Text.Create("Tag1");
    //    var Name2 = Text.Create("Tag2");

    //    var tag1 = Tag.Create(Ulid.NewUlid(), Name1, DateTime.UtcNow);
    //    var tag2 = Tag.Create(Ulid.NewUlid(), Name2, DateTime.UtcNow);



    //    // Act
    //    Ulid result = _tagService.CreateTags([Name1, Name2]);

    //    // Assert
    //    result.Should().NotBe(Ulid.Empty);
    //}

    [Fact]
    public void CreateTag_Should_ThrowArgumentException_WhenSomeNameIsNull()
    {
        // Act
        Action action = () => _tagService.CreateTags([null]);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    #endregion
}
