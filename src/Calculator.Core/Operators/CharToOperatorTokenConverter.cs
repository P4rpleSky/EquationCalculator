using System.Diagnostics.CodeAnalysis;
using EquationCalculator.Core.Operators.Binary;
using EquationCalculator.Core.Operators.Brackets;

namespace EquationCalculator.Core.Operators;

internal static class CharToOperatorTokenConverter
{
    private static readonly IReadOnlyDictionary<char, IOperatorToken> SymbolToTokenMap = new Dictionary<char, IOperatorToken>
    {
        { '+', new AdditionOperatorToken() },
        { '-', new SubtractionOperatorToken() },
        { '*', new MultiplicationOperatorToken() },
        { '/', new DivisionOperatorToken() },
        { '(', new OpeningBracketOperatorToken() },
        { ')', new ClosingBracketOperatorToken() },
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
