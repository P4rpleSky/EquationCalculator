namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal sealed class MultiplicationOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first * second;

    public override string ToString() => "*";
}
