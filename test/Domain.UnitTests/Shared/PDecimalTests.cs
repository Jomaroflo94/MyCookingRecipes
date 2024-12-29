using Domain.Shared;
using FluentAssertions;
using ROP;
using Xunit;

namespace Domain.UnitTests.Shared;
public class PDecimalTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void PDecimal_Should_Throw_ArgumentOutOfRangeException_WhenValueIsLessOrEqualZero(
        decimal value)
    {
        // Act
        Action action = () => PDecimal.Create(value);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void PDecimal_Should_Throw_ArgumentNullException_WhenValueIsNull()
    {
        // Act
        Action action = () => PDecimal.Create(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void PDecimal_Should_ReturnResultPDecimal_WhenValueIsValid()
    {
        // Act
        Result<PDecimal> result = PDecimal.Create((decimal) 1.2);

        // Assert
        Result<PDecimal> test = result.Should().BeOfType<Result<PDecimal>>().Subject;

        test.Errors.Should().ContainSingle().Which.Should().Be(Error.None);
        test.IsSuccess.Should().Be(true);
        test.Value.Should().BeOfType<PDecimal>();
        test.Value.Value.Should().Be((decimal)1.2);
    }
}
