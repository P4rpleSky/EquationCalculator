namespace EquationCalculator.Core.Operators.Binary;

internal sealed class AdditionOperatorToken : IBinaryOperatorToken, IEquatable<AdditionOperatorToken>
{
    private const char Symbol = '+';

    public BinaryOperationDelegate Operation => (first, second) => first + second;

    public override string ToString() => Symbol.ToString();

    #region Equatable Members

    public bool Equals(AdditionOperatorToken? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as AdditionOperatorToken);

    public override int GetHashCode() => Symbol.GetHashCode();

    #endregion
}
