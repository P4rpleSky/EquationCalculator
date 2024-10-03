namespace EquationCalculator.Core.Operators.Binary;

internal sealed class MultiplicationOperatorToken : IBinaryOperatorToken, IEquatable<MultiplicationOperatorToken>
{
    private const char Symbol = '*';

    public BinaryOperationDelegate Operation => (first, second) => first * second;

    public override string ToString() => Symbol.ToString();

    #region Equatable Members

    public bool Equals(MultiplicationOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as MultiplicationOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
