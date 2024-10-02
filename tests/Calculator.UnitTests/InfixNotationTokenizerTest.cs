using Byndyusoft.Calculator.Core;
using Byndyusoft.Calculator.Core.Operands;
using Byndyusoft.Calculator.Core.Operators.Binary;
using Byndyusoft.Calculator.Core.Operators.Brackets;
using Byndyusoft.Calculator.Core.Tokenizers;
using FluentAssertions;
using Xunit;

namespace Byndyusoft.Calculator.UnitTests;

public sealed class InfixNotationTokenizerTest
{
    private static readonly AdditionOperatorToken Plus = new();
    private static readonly SubtractionOperatorToken Minus = new();
    private static readonly MultiplicationOperatorToken Times = new();
    private static readonly DivisionOperatorToken Divide = new();
    private static readonly OpeningBracketOperator Open = new();
    private static readonly ClosingBracketOperator Close = new();

    [Theory]
    [MemberData(nameof(GetValidInfixStrings))]
    public void ShouldTokenizeValidInfixString(string input, IReadOnlyList<IToken> expectedTokens)
    {
        // Arrange

        // Act
        var actualTokens = InfixNotationTokenizer.Parse(input);

        // Assert
        actualTokens.Should().BeEquivalentTo(expectedTokens, options => options.WithStrictOrdering().RespectingRuntimeTypes());
    }

    private static NumberToken Num(decimal number) => NumberToken.Create(number);

    public static IEnumerable<object[]> GetValidInfixStrings()
    {
        return
        [
            ["       ", Array.Empty<IToken>()],
            [ "2 + 4 * 8 - 9 / 5", new List<IToken> { Num(2), Plus, Num(4), Times, Num(8), Minus, Num(9), Divide, Num(5) } ],
            [ "))((*/*+-/", new List<IToken> { Close, Close, Open, Open, Times, Divide, Times, Plus, Minus, Divide } ]
        ];
    }
}
