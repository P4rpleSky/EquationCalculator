using Byndyusoft.Calculator.Core.Operands;
using Byndyusoft.Calculator.Core.Operators;

namespace Byndyusoft.Calculator.Core.Equations;

internal sealed class PostfixEquation : Equation
{
    private static readonly IReadOnlyList<IOperandToken> PrioritizedOperations = new List<IOperandToken>
    {

    }

    private PostfixEquation(IReadOnlyList<IToken> tokens)
        : base(tokens)
    {
    }

    public static PostfixEquation CreateFrom(IReadOnlyList<IToken> tokens)
    {
        var index = 0;

        var output = new Stack<IToken>();
        var operatorStack = new Stack<BinaryOperatorToken222>();

        while (index < tokens.Count)
        {
            var currentToken = tokens[index];

            if (currentToken is NumberToken numberToken)
            {
                output.Push(numberToken);
            }
            else if (currentToken is IOperatorToken operatorToken)
            {
                if (operatorStack.TryPeek(out var lastOperatorToken) && lastOperatorToken > operatorToken)
                {
                    operatorStack.Push(binaryOperatorToken);
                }
                else ()
            }


            index++;
        }

        return new PostfixEquation(tokens);
    }

    public override decimal Calculate()
    {

    }
}
