namespace EquationCalculator.Core.Operators.Brackets;

internal readonly struct OpeningBracketOperatorToken : IBracketToken
{
    public override string ToString() => "(";
}
