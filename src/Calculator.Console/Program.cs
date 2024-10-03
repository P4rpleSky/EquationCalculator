using EquationCalculator.Core.Equations;
using EquationCalculator.Core.Tokenizers;

while (true)
{
    Console.WriteLine();
    Console.WriteLine("-------------------------------");
    Console.WriteLine("Enter an expression to evaluate");
    var input = Console.ReadLine() ?? String.Empty;

    try
    {
        Console.WriteLine($"Infix equation (input): «{input}»");

        var tokens = InfixNotationTokenizer.Parse(input);
        var equation = PostfixEquation.CreateFromInfixSequence(tokens);

        Console.WriteLine($"Postfix equation: «{equation}»");
        Console.WriteLine($"Postfix equation result: «{equation.Result}»");
    }
    catch (InvalidEquationException exception)
    {
        Console.WriteLine($"Cannot calculate the result of an equation: «{exception.Message}»");
    }
}

