namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct SubtractionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first is null ? -1 * second : first.Value - second;

    public override string ToString() => "-";
}
