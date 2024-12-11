using Domain.Shared;
using FluentAssertions;
using ROP;
using Xunit;

namespace Domain.UnitTests.Shared;
public class TextTests
{
    [Fact]
    public void Text_Should_ReturnEmptyError_WhenValueIsEmpty()
    {
        // Act
        Result<Text> result = Text.Create(string.Empty);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(TextErrors.Empty);
    }

    [Fact]
    public void Text_Should_ReturnNullError_WhenValueIsNull()
    {
        // Act
        Result<Text> result = Text.Create(null);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.NullValue);
    }

    [Fact]
    public void Text_Should_ReturnResultText_WhenValueIsValid()
    {
        // Act
        Result<Text> result = Text.Create("Text");

        // Assert
        Result<Text> test = result.Should().BeOfType<Result<Text>>().Subject;

        test.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.None);
        test.IsSuccess.Should().Be(true);
        test.Value.Should().BeOfType<Text>();
        test.Value.Value.Should().Be("Text");
    }
}
