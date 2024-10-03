using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EquationCalculator.Core.Operands;

internal sealed class NumberToken : IToken, IEquatable<NumberToken>
{
    public static readonly NumberToken Zero = new(0);

    private NumberToken(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static NumberToken Create(decimal value) => new(value);

    public static bool TryParse(string value, [NotNullWhen(true)] out NumberToken? token)
    {
        value = value.Replace(',', '.');
        if (!Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedNumber))
        {
            token = null;
            return false;
        }

        token = new NumberToken(parsedNumber);
        return true;
    }

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

    #region Equatable Members

    public bool Equals(NumberToken? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as NumberToken);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(NumberToken? left, NumberToken? right) => Equals(left, right);

    public static bool operator !=(NumberToken? left, NumberToken? right) => !Equals(left, right);

    #endregion
}
