using Domain.Shared;
using FluentAssertions;
using ROP;
using Xunit;

namespace Domain.UnitTests.Shared;
public class PIntTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void PInt_Should_Throw_ArgumentOutOfRangeException_WhenValueIsLessOrEqualZero(
        int value)
    {
        // Act
        Action action = () => PInt.Create(value);

        // Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void PInt_Should_Throw_ArgumentNullException_WhenValueIsNull()
    {
        // Act
        Action action = () => PInt.Create(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void PInt_Should_ReturnResultPInt_WhenValueIsValid()
    {
        // Act
        Result<PInt> result = PInt.Create(1);

        // Assert
        Result<PInt> test = result.Should().BeOfType<Result<PInt>>().Subject;

        test.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.None);
        test.IsSuccess.Should().Be(true);
        test.Value.Should().BeOfType<PInt>();
        test.Value.Value.Should().Be(1);
    }
}
