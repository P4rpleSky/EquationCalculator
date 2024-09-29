using Byndyusoft.Calculator.Core.Operands;
using Byndyusoft.Calculator.Core.Operators;
using Byndyusoft.Calculator.Core.Operators.Binary;

namespace Byndyusoft.Calculator.Core.Equations;

internal sealed class PostfixEquation : Equation
{
    private PostfixEquation(IReadOnlyList<IToken> tokens)
        : base(tokens)
    {
    }

    public static PostfixEquation Create(IReadOnlyList<IToken> tokens)
    {
        var operatorPriorityComparer = new OperatorPriorityComparer();

        var output = new Stack<IToken>();
        var operatorStack = new Stack<IOperatorToken>();

        var index = 0;
        while (index < tokens.Count)
        {
            var currentToken = tokens[index];

            switch (currentToken)
            {
                case NumberToken numberToken:
                    output.Push(numberToken);
                    break;

                case IOperatorToken operatorToken:
                {
                    while (operatorStack.TryPeek(out var lastOperatorToken) && operatorPriorityComparer.Compare(lastOperatorToken, operatorToken) >= 0)
                    {
                        operatorStack.Pop();
                        output.Push(lastOperatorToken);
                    }

                    operatorStack.Push(operatorToken);
                    break;
                }
            }

            index++;
        }

        return new PostfixEquation(tokens);
    }

    public decimal Calculate()
    {
        var operandStack = new Stack<NumberToken>();

        for (var index = 0; index < Tokens.Count; index++)
        {
            var currentToken = Tokens[index];

            switch (currentToken)
            {
                case NumberToken numberToken:
                    operandStack.Push(numberToken);
                    break;

                case IBinaryOperatorToken binaryOperatorToken:
                {
                    if (!operandStack.TryPop(out var firstOperand) || !operandStack.TryPop(out var secondOperand))
                    {
                        throw new InvalidOperationException($"Cannot apply binary operator {binaryOperatorToken.GetType().FullName}: not enough arguments");
                    }

                    var operationResult = binaryOperatorToken.Operation.Invoke(firstOperand.Value, secondOperand.Value);
                    var operationResultToken = NumberToken.Create(operationResult);

                    operandStack.Push(operationResultToken);
                    break;
                }
            }
        }
    }
}
