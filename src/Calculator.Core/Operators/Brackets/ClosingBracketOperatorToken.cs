namespace EquationCalculator.Core.Operators.Brackets;

internal sealed class ClosingBracketOperatorToken : IBracketToken, IEquatable<ClosingBracketOperatorToken>
{
    private const char Symbol = ')';

    public override string ToString() => Symbol.ToString();

    #region Equatable Members

    public bool Equals(ClosingBracketOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as ClosingBracketOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
