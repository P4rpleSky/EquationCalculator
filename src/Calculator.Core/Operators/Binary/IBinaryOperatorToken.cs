namespace EquationCalculator.Core.Operators.Binary;

internal interface IBinaryOperatorToken : IOperatorToken
{
    static abstract BinaryOperationDelegate Operation { get; }
}
