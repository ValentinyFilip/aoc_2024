namespace AdventOfCode.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<int> ToInt(this IEnumerable<string> input) =>
        input.Where(char.IsDigit).Select(c => int.Parse(c.ToString()));
}
