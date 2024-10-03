namespace EquationCalculator.Utilities;

public static class ListExtension
{
    public static void AddIfNotNull<T>(this IList<T> list, T? item)
    {
        if (item is not null)
        {
            list.Add(item);
        }
    }
}
