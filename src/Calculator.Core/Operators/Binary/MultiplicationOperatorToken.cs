namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal readonly struct MultiplicationOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first * second;

    public override string ToString() => "*";
}
