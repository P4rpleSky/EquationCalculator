namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct SubtractionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first - second;

    public override string ToString() => "-";
}
