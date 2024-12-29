using Domain.Shared;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using ROP;
using Xunit;

namespace Domain.UnitTests.Shared;
public class TextTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Text_Should_Throw_ArgumentNullException_WhenValueIsInvalid(
        string? value)
    {
        // Act
        Action action = () => Text.Create(value);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Text_Should_ReturnResultText_WhenValueIsValid()
    {
        // Act
        Result<Text> result = Text.Create("Text");

        // Assert
        Result<Text> test = result.Should().BeOfType<Result<Text>>().Subject;

        test.Errors.Should().ContainSingle().Which.Should().Be(Error.None);
        test.IsSuccess.Should().Be(true);
        test.Value.Should().BeOfType<Text>();
        test.Value.Value.Should().Be("Text");
    }
}
