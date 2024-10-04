namespace EquationCalculator.Core.Operators.Brackets;

internal sealed class OpeningBracketOperatorToken :
    OperatorTokenBase<OpeningBracketOperatorToken>,
    IBracketToken,
    IOperatorTokenDescription
{
    public static char Symbol => '(';
}
