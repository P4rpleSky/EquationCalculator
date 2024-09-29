namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal sealed class MultiplicationOperatorToken :
    BinaryOperatorToken<MultiplicationOperatorToken>,
    IBinaryOperatorTokenDescription
{
    public static char Symbol => '*';

    public static BinaryOperationDelegate Operation => (first, second) => first * second;
}
