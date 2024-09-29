namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal sealed class DivisionOperatorToken :
    BinaryOperatorToken<DivisionOperatorToken>,
    IBinaryOperatorTokenDescription
{
    public static char Symbol => '/';

    public static BinaryOperationDelegate Operation => (first, second) => first / second;
}
