namespace Byndyusoft.Calculator.Core.Operators.Binary;

internal interface IBinaryOperatorTokenDescription : IOperatorTokenDescription
{
    static abstract BinaryOperationDelegate Operation { get; }
}
