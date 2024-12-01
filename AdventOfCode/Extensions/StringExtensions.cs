namespace AdventOfCode.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> GetLines(this string input) => input.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line));

    public static IEnumerable<int> CharsToInt(this string input) =>
        input.Where(char.IsDigit).Select(c => int.Parse(c.ToString()));

    public static int StringToInt(this string input) =>
        int.Parse(input);
}
