namespace AdventOfCode.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<int> ToInt(this IEnumerable<string> input) =>
        input.Select(c => int.Parse(c.ToString()));

    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
    {
        return source
            .Select((value, index) => new { value, index })
            .GroupBy(x => x.index / chunkSize)
            .Select(g => g.Select(x => x.value));
    }

    public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this IEnumerable<T> source, int windowSize)
    {
        var list = source.ToList();
        return Enumerable.Range(0, list.Count - windowSize + 1)
            .Select(i => list.Skip(i).Take(windowSize));
    }

    public static int Product(this IEnumerable<int> source)
    {
        return source.Aggregate(1, (acc, val) => acc * val);
    }

    public static IEnumerable<IEnumerable<string>> SplitByEmptyLines(this IEnumerable<string> source)
    {
        return source
            .Aggregate(new List<List<string>> { new() }, (groups, line) =>
            {
                if (string.IsNullOrWhiteSpace(line))
                    groups.Add(new List<string>());
                else
                    groups.Last().Add(line);
                return groups;
            });
    }

    public static Dictionary<T, int> CountOccurrences<T>(this IEnumerable<T> source) where T : notnull
    {
        return source.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
    }
}
