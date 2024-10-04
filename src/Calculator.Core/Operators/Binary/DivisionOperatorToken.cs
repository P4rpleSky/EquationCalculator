namespace EquationCalculator.Core.Operators.Binary;

internal sealed class DivisionOperatorToken :
    OperatorTokenBase<DivisionOperatorToken>,
    IBinaryOperatorToken,
    IOperatorTokenDescription
{
    public static char Symbol => '/';

    public BinaryOperationDelegate Operation => (first, second) => first / second;
}
