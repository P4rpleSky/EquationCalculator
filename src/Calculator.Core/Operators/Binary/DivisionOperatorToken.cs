namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct DivisionOperatorToken : IBinaryOperatorToken
{
    public static BinaryOperationDelegate Operation => (first, second) => first / second;

    public override string ToString() => "/";
}
