namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal readonly struct DivisionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first / second;

    public override string ToString() => "/";
}
