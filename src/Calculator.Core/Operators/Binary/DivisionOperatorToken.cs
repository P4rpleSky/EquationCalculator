namespace EquationCalculator.Core.Operators.Binary;

internal readonly struct DivisionOperatorToken : IBinaryOperatorToken
{
    public BinaryOperationDelegate Operation => (first, second) =>
    {
        if (first is null)
        {
            throw new ArgumentNullException("Divisible part should not be null");
        }

        if (second == 0)
        {
            throw new InvalidOperationException("Division by zero");
        }

        return first.Value / second;
    };

    public override string ToString() => "/";
}
