namespace EquationCalculator.Core.Operators.Binary;

internal sealed class DivisionOperatorToken : IBinaryOperatorToken, IEquatable<DivisionOperatorToken>
{
    private const char Symbol = '/';

    public BinaryOperationDelegate Operation => (first, second) => first / second;

    public override string ToString() => "/";

    #region Equatable Members

    public bool Equals(DivisionOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as DivisionOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
