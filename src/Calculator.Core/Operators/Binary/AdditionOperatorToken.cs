namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal sealed class AdditionOperatorToken :
    BinaryOperatorToken<AdditionOperatorToken>,
    IBinaryOperatorTokenDescription
{
    public static char Symbol => '+';

    public static BinaryOperationDelegate Operation => (first, second) => first + second;
}
