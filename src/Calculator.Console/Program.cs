using Byndyusoft.Calculator.Core;
using Byndyusoft.Calculator.Core.Equations;
using Byndyusoft.Calculator.Core.Tokenizers;

string? input;

do
{
    Console.WriteLine("Enter an expression to evaluate");
    input = Console.ReadLine();
} while (String.IsNullOrWhiteSpace(input));

var tokens = InfixNotationTokenizer.Parse(input);
var equation = PostfixEquation.Create(tokens);

Console.WriteLine(equation.Result);
