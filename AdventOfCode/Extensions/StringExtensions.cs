namespace AdventOfCode.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> Lines(this string input) => input.Split(['\n', '\r']).Where(line => !string.IsNullOrWhiteSpace(line));

    public static IEnumerable<int> ToInts(this string input, char delimiter)
    {
        return input.Split(delimiter).Select(int.Parse);
    }

    public static IEnumerable<string> ChunkByLength(this string input, int length)
    {
        return Enumerable.Range(0, (input.Length + length - 1) / length)
            .Select(i => input.Substring(i * length, Math.Min(length, input.Length - i * length)));
    }

    public static IEnumerable<int> CharsToInt(this string input) =>
        input.Where(char.IsDigit).Select(c => int.Parse(c.ToString()));

    public static int StringToInt(this string input) =>
        int.Parse(input);

    public static string Reverse(this string input)
    {
        return new string(input.ToCharArray().Reverse().ToArray());
    }

    public static (int x, int y) ToPoint(this string input, char separator)
    {
        string[] parts = input.Split(separator);
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public static int BinaryToInt(this string binary)
    {
        return Convert.ToInt32(binary, 2);
    }

    public static bool IsNumeric(this string input)
    {
        return int.TryParse(input, out _);
    }
}
