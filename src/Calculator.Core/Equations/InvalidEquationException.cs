namespace EquationCalculator.Core.Equations;

internal sealed class InvalidEquationException(string message) : Exception(message);
