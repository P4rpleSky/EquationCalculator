namespace Byndyusoft.Calculator.Core.Equations;

internal abstract class Equation
{
    protected Equation(IReadOnlyList<IToken> tokens)
    {
        Tokens = tokens;
    }

    public IReadOnlyList<IToken> Tokens { get; }
}
