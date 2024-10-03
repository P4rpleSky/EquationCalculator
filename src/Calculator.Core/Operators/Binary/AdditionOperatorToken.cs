namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct AdditionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) => first + second;

    public override string ToString() => "+";
}
