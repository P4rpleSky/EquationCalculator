namespace EquationCalculator.Core.Operators.Binary;

internal interface IBinaryOperatorToken : IOperatorToken
{
    BinaryOperationDelegate Operation { get; }
}
