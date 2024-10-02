namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct AdditionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first is null ? second : first.Value + second;

    public override string ToString() => "+";
}
