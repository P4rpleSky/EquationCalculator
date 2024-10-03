using EquationCalculator.Core.Operands;
using EquationCalculator.Core.Operators;
using EquationCalculator.Core.Operators.Binary;
using EquationCalculator.Core.Operators.Brackets;

namespace EquationCalculator.Core.Equations;

public sealed class PostfixEquation
{
    private static readonly OperatorPriorityComparer OperatorPriorityComparer = new();

    private PostfixEquation(
        IReadOnlyList<IToken> tokens,
        decimal result)
    {
        Tokens = tokens;
        Result = result;
    }

    public IReadOnlyList<IToken> Tokens { get; }

    public decimal Result { get; }

    public static PostfixEquation CreateFromInfixSequence(IReadOnlyList<IToken> tokens)
    {
        tokens = InsertMissingZeros(tokens);
        var postfixTokens = ConvertFromInfixToPostfix(tokens);
        var result = Calculate(postfixTokens);

        return new PostfixEquation(postfixTokens, result);
    }

    private static IReadOnlyList<IToken> InsertMissingZeros(IReadOnlyList<IToken> tokens)
    {
        var result = new List<IToken>(tokens.Count);

        IToken? prevToken = null;
        foreach (var token in tokens)
        {
            if (prevToken is null or OpeningBracketOperatorToken &&
                token is AdditionOperatorToken or SubtractionOperatorToken)
            {
                result.Add(NumberToken.Zero);
            }

            result.Add(token);
            prevToken = token;
        }

        return result;
    }

    private static IReadOnlyList<IToken> ConvertFromInfixToPostfix(IReadOnlyList<IToken> tokens)
    {
        var output = new Queue<IToken>();
        var operatorStack = new Stack<IOperatorToken>();

        var index = 0;
        while (index < tokens.Count)
        {
            var currentToken = tokens[index];

            switch (currentToken)
            {
                case NumberToken numberToken:
                    output.Enqueue(numberToken);
                    break;

                case OpeningBracketOperatorToken openingBracketOperatorToken:
                    operatorStack.Push(openingBracketOperatorToken);
                    break;

                case ClosingBracketOperatorToken:
                    {
                        while (operatorStack.TryPeek(out var prevOperatorToken) &&
                               prevOperatorToken is not OpeningBracketOperatorToken)
                        {
                            operatorStack.Pop();
                            output.Enqueue(prevOperatorToken);
                        }

                        if (!operatorStack.TryPop(out var lastOperatorToken) ||
                            lastOperatorToken is not OpeningBracketOperatorToken)
                        {
                            throw new InvalidEquationException("Incorrect bracket sequence detected");
                        }

                        break;
                    }

                case IOperatorToken operatorToken:
                    {
                        while (operatorStack.TryPeek(out var lastOperatorToken) &&
                               lastOperatorToken is not IBracketToken &&
                               OperatorPriorityComparer.Compare(lastOperatorToken, operatorToken) >= 0)
                        {
                            operatorStack.Pop();
                            output.Enqueue(lastOperatorToken);
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
            if (operatorToken is IBracketToken)
            {
                throw new InvalidEquationException("Incorrect bracket sequence detected");
            }

            output.Enqueue(operatorToken);
        }

        return [.. output];
    }

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
                    if (!operandStack.TryPop(out var secondOperand) || !operandStack.TryPop(out var firstOperand))
                    {
                        throw new InvalidEquationException("Both operands for binary operator should be specified");
                    }

                    ValidateBinaryOperationTokenProcessing(binaryOperatorToken, firstOperand, secondOperand);
                    var operationResultToken = CreateNumberToken(binaryOperatorToken, firstOperand, secondOperand);
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

    private static void ValidateBinaryOperationTokenProcessing(
        IBinaryOperatorToken binaryOperatorToken,
        NumberToken firstOperand,
        NumberToken secondOperand)
    {
        if (binaryOperatorToken is DivisionOperatorToken &&
            secondOperand == NumberToken.Zero)
        {
            throw new InvalidEquationException("Division by zero isn't allowed");
        }
    }

    private static NumberToken CreateNumberToken(
        IBinaryOperatorToken token,
        NumberToken firstOperand,
        NumberToken secondOperand)
    {
        var result = token.Operation.Invoke(firstOperand.Value, secondOperand.Value);
        return NumberToken.Create(result);
    }
}
