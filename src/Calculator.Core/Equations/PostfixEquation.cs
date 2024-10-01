using Byndyusoft.Calculator.Core.Operands;
using Byndyusoft.Calculator.Core.Operators;
using Byndyusoft.Calculator.Core.Operators.Binary;
using Byndyusoft.Calculator.Core.Tokenizers;

namespace Byndyusoft.Calculator.Core.Equations;

public sealed class PostfixEquation
{
    private PostfixEquation(IReadOnlyList<IToken> tokens)
    {
        Tokens = tokens;
        Result = Calculate(tokens);
    }

    public IReadOnlyList<IToken> Tokens { get; }

    public decimal Result { get; }

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

        while (operatorStack.TryPop(out var operatorToken))
        {
            output.Push(operatorToken);
        }

        var outputTokens = output.Reverse().ToList();
        return new PostfixEquation(outputTokens);
    }

    public override string ToString() => String.Join(' ', Tokens);

    private static decimal Calculate(IReadOnlyList<IToken> tokens)
    {
        var operandStack = new Stack<NumberToken>();

        foreach (var currentToken in tokens)
        {
            switch (currentToken)
            {
                case NumberToken numberToken:
                    operandStack.Push(numberToken);
                    break;

                case IBinaryOperatorToken binaryOperatorToken:
                {
                    if (!operandStack.TryPop(out var firstOperand) ||
                        !operandStack.TryPop(out var secondOperand))
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

        // TODO
        return operandStack.Single().Value;
    }
}
