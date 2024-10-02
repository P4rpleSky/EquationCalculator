using Byndyusoft.Calculator.Core.Operators.Binary;
using Byndyusoft.Calculator.Core.Operators.Brackets;

namespace Byndyusoft.Calculator.Core.Operators;

internal sealed class OperatorPriorityComparer : IComparer<IOperatorToken>
{
    private static readonly IReadOnlyDictionary<Type, int> TokenToPriorityMap = new Dictionary<Type, int>
    {
        { typeof(OpeningBracketOperator), 10 },
        { typeof(ClosingBracketOperator), 10 },
        { typeof(MultiplicationOperatorToken), 50 },
        { typeof(DivisionOperatorToken), 50 },
        { typeof(AdditionOperatorToken), 100 },
        { typeof(SubtractionOperatorToken), 100 }
    };

    public int Compare(IOperatorToken? firstToken, IOperatorToken? secondToken)
    {
        if (firstToken is null || secondToken is null)
        {
            throw new InvalidOperationException("Tokens to compare should not be null");
        }

        var firstTokenPriority = GetTokenPriority(firstToken.GetType());
        var secondTokenPriority = GetTokenPriority(secondToken.GetType());

        return secondTokenPriority.CompareTo(firstTokenPriority);
    }

    internal static int GetTokenPriority(Type tokenType)
    {
        if (!TokenToPriorityMap.TryGetValue(tokenType, out var priority))
        {
            throw new InvalidOperationException($"Invalid operation type to compare: «{tokenType.FullName}»");
        }

        return priority;
    }
}
