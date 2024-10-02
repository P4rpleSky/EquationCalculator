namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct MultiplicationOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) =>
    {
        if (first is null)
        {
            throw new ArgumentNullException("Multiplier should not be null");
        }

        return first.Value * second;
    };

    public override string ToString() => "*";
}
