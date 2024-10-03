namespace EquationCalculator.Core.Equations;

public sealed class InvalidEquationException(string message) : Exception(message);
