namespace EquationCalculator.Core.Operators.Binary;

internal sealed class AdditionOperatorToken :
    OperatorTokenBase<AdditionOperatorToken>,
    IBinaryOperatorToken,
    IOperatorTokenDescription
{
    public static char Symbol => '+';

    public BinaryOperationDelegate Operation => (first, second) => first + second;
}
