using EquationCalculator.Core.Equations;
using EquationCalculator.Core.Tokenizers;

string? input;

do
{
    Console.WriteLine("Enter an expression to evaluate");
    input = Console.ReadLine();
} while (String.IsNullOrWhiteSpace(input));

var tokens = InfixNotationTokenizer.Parse(input);
var equation = PostfixEquation.CreateFromInfixSequence(tokens);

Console.WriteLine(equation.Result);
