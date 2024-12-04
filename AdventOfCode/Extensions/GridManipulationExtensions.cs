namespace AdventOfCode.Extensions;

public static class GridManipulationExtensions
{
    public static IEnumerable<IEnumerable<int>> ToGrid(this IEnumerable<string> input)
    {
        return input.Select(line => line.Select(c => c - '0'));
    }

    public static IEnumerable<IEnumerable<char>> ToGridChar(this IEnumerable<string> input)
    {
        return input.Select(line => line.Select(c => c));
    }

    public static IEnumerable<(int x, int y)> Neighbors(this (int x, int y) point, int width, int height)
    {
        (int x, int y) = point;
        var deltas = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) }; // Left, Right, Up, Down

        return deltas.Select(d => (x + d.Item1, y + d.Item2))
            .Where(p => p.Item1 >= 0 && p.Item2 >= 0 && p.Item1 < width && p.Item2 < height);
    }

    public static IEnumerable<(int x, int y)> AllNeighbors(this (int x, int y) point, int width, int height)
    {
        return point.Neighbors(width, height).Concat(point.DiagonalNeighbors(width, height));
    }

    public static IEnumerable<(int x, int y)> DiagonalNeighbors(this (int x, int y) point, int width, int height)
    {
        (int x, int y) = point;
        var deltas = new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) }; // Top-Left, Top-Right, Bottom-Left, Bottom-Right

        return deltas.Select(d => (x + d.Item1, y + d.Item2))
            .Where(p => p.Item1 >= 0 && p.Item2 >= 0 && p.Item1 < width && p.Item2 < height);
    }

    public static (int x, int y) Rotate(this (int x, int y) point, int degrees)
    {
        double radians = Math.PI * degrees / 180.0;
        int x = (int)(point.x * Math.Cos(radians) - point.y * Math.Sin(radians));
        int y = (int)(point.x * Math.Sin(radians) + point.y * Math.Cos(radians));
        return (x, y);
    }
}
