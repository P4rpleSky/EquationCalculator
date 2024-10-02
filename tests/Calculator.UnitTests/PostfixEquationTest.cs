using EquationCalculator.Core;
using EquationCalculator.Core.Equations;
using EquationCalculator.Core.Operands;
using EquationCalculator.Core.Operators.Binary;
using FluentAssertions;
using Xunit;

namespace EquationCalculator.UnitTests;

public sealed class PostfixEquationTest
{
    private static readonly AdditionOperatorToken Plus = new();
    private static readonly SubtractionOperatorToken Minus = new();
    private static readonly MultiplicationOperatorToken Times = new();
    private static readonly DivisionOperatorToken Divide = new();

    [Theory]
    [MemberData(nameof(ValidPostfixEquations))]
    public void ShouldCreateAndEvaluateValidEquation(IReadOnlyList<IToken> tokens, decimal expectedResult)
    {
        // Arrange

        // Act
        var actualEquation = PostfixEquation.Create(tokens);

        // Assert
        actualEquation.Result.Should().Be(expectedResult);
    }

    public static IEnumerable<object[]> ValidPostfixEquations()
    {
        return
        [
            [new List<IToken> { Num(2), Plus, Num(2) }, 4],
            [new List<IToken> { Num(3), Plus, Num(3), Times, Num(3) }, 12],
            [new List<IToken> { Minus, Num(1), Divide, Num(100) }, -0.01],
        ];
    }

    private static NumberToken Num(decimal number) => NumberToken.Create(number);
}
