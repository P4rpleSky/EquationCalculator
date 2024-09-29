using System.Diagnostics.CodeAnalysis;

namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal abstract class BinaryOperatorToken<T> : IOperatorToken<T>
    where T : BinaryOperatorToken<T>, IBinaryOperatorTokenDescription, new()
{
    public static bool TryParse(char symbol, [NotNullWhen(true)] out T? token)
    {
        if (symbol != T.Symbol)
        {
            token = null;
            return false;
        }

        token = new T();
        return true;
    }
}
