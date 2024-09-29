using System.Diagnostics.CodeAnalysis;
using Byndyusoft.Calculator.Core.Operators.Binary;

namespace Byndyusoft.Calculator.Core.Operators;

internal static class OperatorTokenParser
{
    private static readonly IReadOnlyDictionary<char, IOperatorToken> SymbolToTokenMap = new Dictionary<char, IOperatorToken>
    {
        { '+', new AdditionOperatorToken() },
        { '-', new SubtractionOperatorToken() },
        { '*', new MultiplicationOperatorToken() },
        { '/', new DivisionOperatorToken() },
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
