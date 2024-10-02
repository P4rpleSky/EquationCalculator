using System.Text;
using EquationCalculator.Core.Operands;
using EquationCalculator.Core.Operators;
using Utilities;

namespace EquationCalculator.Core.Tokenizers;

public static class InfixNotationTokenizer
{
    public static IReadOnlyList<IToken> Parse(string input)
    {
        input = input.Replace(" ", String.Empty);

        if (String.IsNullOrWhiteSpace(input))
        {
            return [];
        }

        var numberSymbolsBuffer = new StringBuilder();
        var tokens = new List<IToken>();

        foreach (var symbol in input)
        {
            if (!CharToOperatorTokenConverter.TryParse(symbol, out var operationToken))
            {
                numberSymbolsBuffer.Append(symbol);
                continue;
            }

            var numberToken = FlushBuffer(numberSymbolsBuffer);

            tokens.AddIfNotNull(numberToken);
            tokens.Add(operationToken);
        }

        var lastNumberToken = FlushBuffer(numberSymbolsBuffer);
        tokens.AddIfNotNull(lastNumberToken);

        return tokens;
    }

    private static NumberToken? FlushBuffer(StringBuilder numberSymbolsBuffer)
    {
        var unparsedNumber = numberSymbolsBuffer.ToString();
        numberSymbolsBuffer.Clear();

        return NumberToken.TryParse(unparsedNumber, out var numberToken)
            ? numberToken
            : null;
    }
}
