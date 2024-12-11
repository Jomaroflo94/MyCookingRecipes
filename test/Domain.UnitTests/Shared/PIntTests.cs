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
    public void PInt_Should_ReturnNegativeError_WhenValueIsLessOrEqualZero(int value)
    {
        // Act
        Result<PInt> result = PInt.Create(value);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(PIntErrors.NegativeOrZero);
    }

    [Fact]
    public void PInt_Should_ReturnNullError_WhenValueIsNull()
    {
        // Act
        Result<PInt> result = PInt.Create(null);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.NullValue);
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
