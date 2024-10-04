namespace EquationCalculator.Core.Operators.Brackets;

internal sealed class ClosingBracketOperatorToken :
    OperatorTokenBase<ClosingBracketOperatorToken>,
    IBracketToken,
    IOperatorTokenDescription
{
    public static char Symbol => ')';
}
