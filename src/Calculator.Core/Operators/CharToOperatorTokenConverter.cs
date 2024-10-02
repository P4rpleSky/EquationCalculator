using System.Diagnostics.CodeAnalysis;
using Byndyusoft.Calculator.Core.Operators.Binary;
using Byndyusoft.Calculator.Core.Operators.Brackets;

namespace Byndyusoft.Calculator.Core.Operators;

internal static class CharToOperatorTokenConverter
{
    private static readonly IReadOnlyDictionary<char, IOperatorToken> SymbolToTokenMap = new Dictionary<char, IOperatorToken>
    {
        { '+', new AdditionOperatorToken() },
        { '-', new SubtractionOperatorToken() },
        { '*', new MultiplicationOperatorToken() },
        { '/', new DivisionOperatorToken() },
        { '(', new OpeningBracketOperator() },
        { ')', new ClosingBracketOperator() },
    };

    public static bool TryParse(char symbol, [NotNullWhen(true)] out IOperatorToken? token)
    {
        if (SymbolToTokenMap.TryGetValue(symbol, out token))
        {
            return true;
        }

        token = null;
        return false;
    }
}
