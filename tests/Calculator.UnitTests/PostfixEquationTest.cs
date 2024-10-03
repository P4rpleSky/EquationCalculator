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
    [MemberData(nameof(GetValidPostfixEquations))]
    public void ShouldCreateAndEvaluateValidEquation(TokensList tokens, decimal expectedResult)
    {
        // Arrange

        // Act
        var actualEquation = PostfixEquation.Create(tokens.Value);

        // Assert
        actualEquation.Result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetInvalidPostfixEquations))]
    public void ShouldThrowOnInvalidEquation(TokensList tokens)
    {
        // Arrange

        // Act
        var createEquation = () => PostfixEquation.Create(tokens.Value);

        // Assert
        createEquation.Should().Throw<InvalidEquationException>();
    }

    public static IEnumerable<object[]> GetValidPostfixEquations()
    {
        return
        [
            [new TokensList(), 0],
            [new TokensList(Num(97.982m)), 97.982],
            [new TokensList(Num(2), Plus, Num(2)), 4],
            [new TokensList(Plus, Num(2), Plus, Num(3), Times, Num(4)), 14],
            [new TokensList(Num(2), Times, Num(35.589m), Plus, Num(4)), 75.178],
            [new TokensList(Minus, Num(1), Divide, Num(100)), -0.01],
        ];
    }

    public static IEnumerable<object[]> GetInvalidPostfixEquations()
    {
        return
        [
            [new TokensList(Plus)],
            [new TokensList(Minus)],
            [new TokensList(Num(2), Times)],
            [new TokensList(Divide, Num(9.9m))],
            [new TokensList(Num(978), Divide, NumberToken.Zero)],
            [new TokensList(Num(978), Num(978))],
        ];
    }

    private static NumberToken Num(decimal number) => NumberToken.Create(number);

    // Only for changing display name in "TheoryAttribute"
    public sealed class TokensList(params IToken[] tokens)
    {
        public IReadOnlyList<IToken> Value { get; } = tokens;

        public override string ToString() => String.Join(' ', Value);
    }
}
