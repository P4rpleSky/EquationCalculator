using ExpressionCalculator.Core.Operands;
using FluentAssertions;
using Xunit;

namespace ExpressionCalculator.UnitTests;

public sealed class NumberTokenTest
{
    [Theory]
    [InlineData("92485", 92485d)]
    [InlineData("0.8836", 0.8836d)]
    [InlineData("2,345", 2.345d)]
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
