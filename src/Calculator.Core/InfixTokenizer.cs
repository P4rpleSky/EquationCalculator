using System.Text;
using Byndyusoft.Calculator.Core.Operands;
using Byndyusoft.Calculator.Core.Operators;

namespace Byndyusoft.Calculator.Core;

public static class InfixTokenizer
{
    public static IReadOnlyList<IToken> Parse(string input)
    {
        input = input.Replace(" ", String.Empty);

        if (String.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input string should not be empty");
        }

        var numberSymbolsBuffer = new StringBuilder();
        var tokens = new List<IToken>();

        foreach (var symbol in input)
        {
            if (!OperatorTokenParser.TryParse(symbol, out var operationToken))
            {
                numberSymbolsBuffer.Append(symbol);
                continue;
            }

            var numberToken = FlushBuffer(numberSymbolsBuffer);

            tokens.Add(numberToken);
            tokens.Add(operationToken);
        }

        var lastNumberToken = FlushBuffer(numberSymbolsBuffer);
        tokens.Add(lastNumberToken);

        return tokens;
    }

    private static NumberToken FlushBuffer(StringBuilder numberSymbolsBuffer)
    {
        var unparsedNumber = numberSymbolsBuffer.ToString();
        if (!NumberToken.TryParse(unparsedNumber, out var numberToken))
        {
            throw new InvalidOperationException($"Invalid number format: «{unparsedNumber}»");
        }

        numberSymbolsBuffer.Clear();
        return numberToken;
    }
}
