namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct SubtractionOperatorToken : IBinaryOperatorToken
{
    public static BinaryOperationDelegate Operation => (first, second) => first - second;

    public override string ToString() => "-";
}
