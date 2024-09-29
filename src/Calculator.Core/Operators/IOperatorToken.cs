using System.Diagnostics.CodeAnalysis;

namespace Byndyusoft.Calculator.Core.Operators;

internal interface IOperatorToken : IToken
{ }

internal interface IOperatorToken<T> : IOperatorToken
    where T : IOperatorToken<T>
{
    static abstract bool TryCreate(char symbol, [NotNullWhen(true)] out T? token);
}
