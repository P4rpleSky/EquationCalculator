using Byndyusoft.Calculator.Core.Tokenizers;
using Xunit;

namespace Calculator.UnitTests;

public sealed class PostfixEquationTest
{
    [Theory]
    [MemberData(nameof(ValidPostfixEquations))]
    public void ShouldCreateAndEvaluateValidEquation(IReadOnlyList<IToken> tokens)
    {
        // Arrange

        // Act

        // Assert
    }

    public static IEnumerable<object[]> ValidPostfixEquations()
    {
        return new List<object[]>
        {
            new []
            {
                []
            }
        }
    }
}
