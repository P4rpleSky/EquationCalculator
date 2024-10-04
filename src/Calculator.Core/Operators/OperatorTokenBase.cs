namespace EquationCalculator.Core.Operators;

internal abstract class OperatorTokenBase<T> : IOperatorToken, IEquatable<T>
    where T : OperatorTokenBase<T>, IOperatorTokenDescription
{
    public override string ToString() => T.Symbol.ToString();

    #region Equatable Members

    public bool Equals(T? other) => other is not null;

    public override bool Equals(object? obj) => Equals(obj as T);

    public override int GetHashCode() => T.Symbol.GetHashCode();

    #endregion
}
