namespace EquationCalculator.Core.Operators.Binary;

internal sealed class SubtractionOperatorToken :
    OperatorTokenBase<SubtractionOperatorToken>,
    IBinaryOperatorToken,
    IOperatorTokenDescription
{
    public static char Symbol => '-';

    public BinaryOperationDelegate Operation => (first, second) => first - second;
}
