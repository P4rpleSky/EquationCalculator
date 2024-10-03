using EquationCalculator.Core;
using EquationCalculator.Core.Equations;
using EquationCalculator.Core.Operands;
using EquationCalculator.Core.Operators.Binary;
using EquationCalculator.Core.Operators.Brackets;
using FluentAssertions;
using Xunit;

namespace EquationCalculator.UnitTests;

public sealed class PostfixEquationTest
{
    private static readonly AdditionOperatorToken Plus = new();
    private static readonly SubtractionOperatorToken Minus = new();
    private static readonly MultiplicationOperatorToken Times = new();
    private static readonly DivisionOperatorToken Divide = new();
    private static readonly OpeningBracketOperatorToken Open = new();
    private static readonly ClosingBracketOperatorToken Close = new();

    [Theory]
    [MemberData(nameof(GetValidInfixEquations))]
    public void ShouldCreateAndEvaluateValidEquation(TokensList tokens, decimal expectedResult)
    {
        // Arrange
        var a1 = new AdditionOperatorToken();
        var a2 = new AdditionOperatorToken();
        var a3 = new MultiplicationOperatorToken();
        if (a1.Equals(a2) || a2.Equals(a3) || a2.Operation.Equals(a3.Operation))
        {

        }

        // Act
        var actualEquation = PostfixEquation.CreateFromInfixSequence(tokens.Value);

        // Assert
        actualEquation.Result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetInvalidInfixEquations))]
    public void ShouldThrowOnInvalidEquation(TokensList tokens)
    {
        // Arrange

        // Act
        var createEquation = () => PostfixEquation.CreateFromInfixSequence(tokens.Value);

        // Assert
        createEquation.Should().Throw<InvalidEquationException>();
    }

    public static IEnumerable<object[]> GetValidInfixEquations()
    {
        return
        [
            [new TokensList(), 0],
            [new TokensList(Open, Open, Num(9), Close, Close), 9],
            [new TokensList(Num(97.982m)), 97.982],
            [new TokensList(Num(1), Plus, Num(2), Minus, Num(3)), 0],
            [new TokensList(Plus, Num(2), Plus, Num(3), Times, Num(4)), 14],
            [new TokensList(Num(2), Times, Num(35.589m), Plus, Num(4)), 75.178],
            [new TokensList(Minus, Num(1), Divide, Num(100)), -0.01],
            [new TokensList(Open, Plus, Num(2), Plus, Num(3), Close, Times, Num(4)), 20],
            [new TokensList(Num(3.5m), Times, Open, Plus, Num(2), Plus, Num(3), Close, Times, Num(4)), 70],
            // 9 * 2 + 3 + 3 * (-10+1000) / 1 + ((4 + 4) * 4) = 3023
            [new TokensList(Num(9), Times, Num(2), Plus, Num(3), Plus, Num(3), Times, Open, Minus, Num(10), Plus, Num(1000), Close, Divide, Num(1), Plus, Open, Open, Num(4), Plus, Num(4), Close, Times, Num(4), Close), 3023],
        ];
    }

    public static IEnumerable<object[]> GetInvalidInfixEquations()
    {
        return
        [
            [new TokensList(Plus)],
            [new TokensList(Minus)],
            [new TokensList(Num(2), Times)],
            [new TokensList(Divide, Num(9.9m))],
            [new TokensList(Num(978), Divide, NumberToken.Zero)],
            [new TokensList(Num(978), Num(978))],
            [new TokensList(Open, Num(978), Plus, Num(4), Close, Open)],
        ];
    }

    private static NumberToken Num(decimal number) => NumberToken.Create(number);

    // Only for changing display name in "TheoryAttribute"
    public sealed class TokensList(params IToken[] tokens)
    {
        public IReadOnlyList<IToken> Value { get; } = tokens;

        public override string ToString() => "«" + String.Join(' ', Value) + "»";
    }
}
