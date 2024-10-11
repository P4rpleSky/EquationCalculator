using EquationCalculator.Core.Equations;

namespace EquationCalculator.Core.Operators.Binary;

internal sealed class DivisionOperatorToken :
    OperatorTokenBase<DivisionOperatorToken>,
    IBinaryOperatorToken,
    IOperatorTokenDescription
{
    public static char Symbol => '/';

    public BinaryOperationDelegate Operation => (first, second) =>
    {
        if (second == 0)
        {
            throw new InvalidEquationException("Division by zero isn't allowed");
        }

        return first / second;
    };
}
