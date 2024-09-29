using Byndyusoft.Calculator.Core;
using Byndyusoft.Calculator.Core.Equations;

string? input;

do
{
    Console.WriteLine("Enter an expression to evaluate");
    input = Console.ReadLine();
} while (String.IsNullOrWhiteSpace(input));

var tokens = InfixTokenizer.Parse(input);
var equation = PostfixEquation.Create(tokens);

Console.WriteLine(equation.Result);
