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
    public void PDecimal_Should_ReturnNegativeError_WhenValueIsLessOrEqualZero(decimal value)
    {
        // Act
        Result<PDecimal> result = PDecimal.Create(value);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(PDecimalErrors.NegativeOrZero);
    }

    [Fact]
    public void PDecimal_Should_ReturnNullError_WhenValueIsNull()
    {
        // Act
        Result<PDecimal> result = PDecimal.Create(null);

        // Assert
        result.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.NullValue);
    }

    [Fact]
    public void PDecimal_Should_ReturnResultPDecimal_WhenValueIsValid()
    {
        // Act
        Result<PDecimal> result = PDecimal.Create((decimal) 1.2);

        // Assert
        Result<PDecimal> test = result.Should().BeOfType<Result<PDecimal>>().Subject;

        test.Errors.Should().ContainSingle()
               .Which.Should().Be(Error.None);
        test.IsSuccess.Should().Be(true);
        test.Value.Should().BeOfType<PDecimal>();
        test.Value.Value.Should().Be((decimal)1.2);
    }
}
