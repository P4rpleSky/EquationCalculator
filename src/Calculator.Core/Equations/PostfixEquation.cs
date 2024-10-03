using EquationCalculator.Core.Operands;
using EquationCalculator.Core.Operators;
using EquationCalculator.Core.Operators.Binary;

namespace EquationCalculator.Core.Equations;

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
                        while (operatorStack.TryPeek(out var lastOperatorToken) &&
                               operatorPriorityComparer.Compare(lastOperatorToken, operatorToken) >= 0)
                        {
                            operatorStack.Pop();
                            output.Push(lastOperatorToken);
                        }

                        operatorStack.Push(operatorToken);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentToken));
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
        if (!tokens.Any())
        {
            return 0;
        }

        var operandStack = new Stack<NumberToken>();

        foreach (var currentToken in tokens)
        {
            switch (currentToken)
            {
                case NumberToken numberToken:
                    operandStack.Push(numberToken);
                    break;

                case IBinaryOperatorToken binaryOperatorToken:
                    var secondOperand = operandStack.TryPop(out var operand) ? operand : null;
                    var firstOperand = operandStack.TryPop(out operand) ? operand : null;
                    var operationResultToken = ProcessBinaryOperationToken(binaryOperatorToken, firstOperand, secondOperand);
                    operandStack.Push(operationResultToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentToken));
            }
        }

        if (operandStack.Count != 1)
        {
            throw new InvalidEquationException("Final token sequence should contain only one operand");
        }

        return operandStack.Single().Value;
    }

    private static NumberToken ProcessBinaryOperationToken(
        IBinaryOperatorToken binaryOperatorToken,
        NumberToken? firstOperand,
        NumberToken? secondOperand)
    {
        return binaryOperatorToken switch
        {
            AdditionOperatorToken => ProcessAddition(firstOperand, secondOperand),
            SubtractionOperatorToken => ProcessSubtraction(firstOperand, secondOperand),
            MultiplicationOperatorToken => ProcessMultiplication(firstOperand, secondOperand),
            DivisionOperatorToken => ProcessDivision(firstOperand, secondOperand),
            _ => throw new ArgumentOutOfRangeException(nameof(binaryOperatorToken))
        };
    }

    private static NumberToken ProcessAddition(NumberToken? firstOperand, NumberToken? secondOperand)
    {
        firstOperand ??= NumberToken.Zero;

        if (secondOperand is null)
        {
            throw new InvalidEquationException("Second operand should be specified for the addition operator");
        }

        return CreateNumberToken<AdditionOperatorToken>(firstOperand, secondOperand);
    }

    private static NumberToken ProcessSubtraction(NumberToken? firstOperand, NumberToken? secondOperand)
    {
        firstOperand ??= NumberToken.Zero;

        if (secondOperand is null)
        {
            throw new InvalidEquationException("Second operand should be specified for the subtraction operator");
        }

        return CreateNumberToken<SubtractionOperatorToken>(firstOperand, secondOperand);
    }

    private static NumberToken ProcessMultiplication(NumberToken? firstOperand, NumberToken? secondOperand)
    {
        if (firstOperand is null || secondOperand is null)
        {
            throw new InvalidEquationException("Both arguments should be specified for the multiplication operator");
        }

        return CreateNumberToken<MultiplicationOperatorToken>(firstOperand, secondOperand);
    }

    private static NumberToken ProcessDivision(NumberToken? firstOperand, NumberToken? secondOperand)
    {
        if (firstOperand is null || secondOperand is null)
        {
            throw new InvalidEquationException("Both arguments should be specified for the division operator");
        }

        if (secondOperand == NumberToken.Zero)
        {
            throw new InvalidEquationException("Division by zero isn't allowed");
        }

        return CreateNumberToken<DivisionOperatorToken>(firstOperand, secondOperand);
    }

    private static NumberToken CreateNumberToken<T>(NumberToken firstOperand, NumberToken secondOperand)
        where T : IBinaryOperatorToken
    {
        var result = T.Operation.Invoke(firstOperand.Value, secondOperand.Value);
        return NumberToken.Create(result);
    }
}
