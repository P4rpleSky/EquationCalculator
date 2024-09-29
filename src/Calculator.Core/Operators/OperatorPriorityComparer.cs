using Byndyusoft.Calculator.Core.Operators.Binary;

namespace Byndyusoft.Calculator.Core.Operators;

internal sealed class OperatorPriorityComparer : IComparer<IOperatorToken>
{
    private static readonly IReadOnlyDictionary<Type, int> TokenToPriorityMap = new Dictionary<Type, int>
    {
        { typeof(MultiplicationOperatorToken), 50 },
        { typeof(SubtractionOperatorToken), 50 },
        { typeof(AdditionOperatorToken), 100 },
        { typeof(SubtractionOperatorToken), 100 }
    };

    public int Compare(IOperatorToken? firstToken, IOperatorToken? secondToken)
    {
        if (firstToken is null || secondToken is null)
        {
            throw new InvalidOperationException("Tokens to compare should not be null");
        }

        var firstTokenPriority = GetTokenPriority(firstToken);
        var secondTokenPriority = GetTokenPriority(secondToken);

        return firstTokenPriority.CompareTo(secondTokenPriority);
    }

    private static int GetTokenPriority(IOperatorToken token)
    {
        var tokenType = token.GetType();
        if (!TokenToPriorityMap.TryGetValue(tokenType, out var priority))
        {
            throw new InvalidOperationException($"Invalid operation type to compare: «{tokenType.FullName}»");
        }

        return priority;
    }
}
