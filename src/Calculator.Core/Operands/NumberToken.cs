﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Byndyusoft.Calculator.Core.Operands;

internal sealed class NumberToken : IToken
{
    private NumberToken(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static bool TryParse(string value, [NotNullWhen(true)] out NumberToken? token)
    {
        if (!Decimal.TryParse(value, CultureInfo.InvariantCulture, out var parsedNumber))
        {
            token = null;
            return false;
        }

        token = new NumberToken(parsedNumber);
        return true;
    }
}
