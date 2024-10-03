namespace EquationCalculator.Core.Operators.Binary;

internal sealed class SubtractionOperatorToken : IBinaryOperatorToken, IEquatable<SubtractionOperatorToken>
{
    private const char Symbol = '-';

    public BinaryOperationDelegate Operation => (first, second) => first - second;

    public override string ToString() => Symbol.ToString();

    #region Equatable Members

    public bool Equals(SubtractionOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as SubtractionOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
