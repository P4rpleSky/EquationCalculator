namespace EquationCalculator.Core.Operators.Brackets;

internal sealed class OpeningBracketOperatorToken : IBracketToken, IEquatable<OpeningBracketOperatorToken>
{
    private const char Symbol = '(';

    public override string ToString() => Symbol.ToString();

    #region Equatable Members

    public bool Equals(OpeningBracketOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as OpeningBracketOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
