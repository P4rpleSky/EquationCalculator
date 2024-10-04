namespace EquationCalculator.Core.Operators.Binary;

internal sealed class MultiplicationOperatorToken :
    OperatorTokenBase<MultiplicationOperatorToken>,
    IBinaryOperatorToken,
    IOperatorTokenDescription
{
    public static char Symbol => '*';

    public BinaryOperationDelegate Operation => (first, second) => first * second;
}
