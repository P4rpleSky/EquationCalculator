namespace EquationCalculator.Core.Operators.Brackets;

internal readonly struct ClosingBracketOperatorToken : IBracketToken
{
    public override string ToString() => ")";
}
