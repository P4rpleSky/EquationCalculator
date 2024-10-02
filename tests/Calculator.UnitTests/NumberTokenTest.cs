using EquationCalculator.Core.Operands;
using FluentAssertions;
using Xunit;

namespace EquationCalculator.UnitTests;

public sealed class NumberTokenTest
{
    [Theory]
    [InlineData("92485", 92485)]
    [InlineData("0.8836", 0.8836)]
    [InlineData("2,345", 2.345)]
    public void ShouldParseValidInput(string input, decimal expectedNum)
    {
        // Arrange

        // Act
        var result = NumberToken.TryParse(input, out var actualToken);

        // Assert
        result.Should().BeTrue();
        actualToken?.Value.Should().Be(expectedNum);
    }

    [Theory]
    [InlineData("NaN")]
    [InlineData("0.88a36")]
    [InlineData("+")]
    public void ShouldNotParseInvalidInput(string input)
    {
        // Arrange

        // Act
        var result = NumberToken.TryParse(input, out var actualToken);

        // Assert
        result.Should().BeFalse();
        actualToken.Should().BeNull();
    }
}
