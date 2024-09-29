namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal sealed class SubtractionOperatorToken :
    BinaryOperatorToken<SubtractionOperatorToken>,
    IBinaryOperatorTokenDescription
{
    public static char Symbol => '-';

    public static BinaryOperationDelegate Operation => (first, second) => first - second;
}
