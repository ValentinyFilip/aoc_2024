namespace AdventOfCode.Days;

public sealed class Day04 : BaseDay
{
    private readonly string _input;

    private readonly (int dx, int dy)[] _directionsP1 =
    [
        (-1, 0), (1, 0), (0, -1), (0, 1),
        (-1, -1), (-1, 1), (1, -1), (1, 1)
    ];

    private readonly (int dx, int dy)[] _directionsP2 =
    [
        (-1, -1), (-1, 1), (1, -1), (1, 1)
    ];

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        char[][] grid = _input.Lines().ToGridChar().Select(row => row.ToArray()).ToArray();

        int rows = grid.Length;
        int columns = grid[0].Length;

        int result = Enumerable.Range(0, rows)
            .SelectMany(x => Enumerable.Range(0, columns).Select(y => (x, y)))
            .SelectMany(cell => _directionsP1.Select(dir => (cell, dir)))
            .Count(item =>
            {
                return Enumerable.Range(0, 4)
                    .Select(i => (i, x: item.cell.x + i * item.dir.dx, y: item.cell.y + i * item.dir.dy))
                    .All(pos => pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < columns && grid[pos.x][pos.y] == "XMAS"[pos.i]);
            });
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        char[][] grid = _input.Lines().ToGridChar().Select(row => row.ToArray()).ToArray();

        int rows = grid.Length;
        int columns = grid[0].Length;

        int result = Enumerable.Range(1, rows - 2)
            .SelectMany(x => Enumerable.Range(1, columns - 2).Select(y => (x, y)))
            .Count(center =>
            {
                if (grid[center.x][center.y] != 'A') return false;

                var patterns = new[]
                {
                    new { firstChar = 'S', secondChar = 'M' },
                    new { firstChar = 'M', secondChar = 'S' }
                };

                return patterns.Any(pattern =>
                    _directionsP2.Select((dir, i) =>
                    {
                        (int dx, int dy) = dir;
                        var pos = (x: center.x + dx, y: center.y + dy);
                        // Check if diagonal positions match the pattern
                        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < columns &&
                               grid[pos.x][pos.y] == (i % 2 == 0 ? pattern.firstChar : pattern.secondChar);
                    }).All(match => match));
            });
        return new ValueTask<string>(result.ToString());
    }
}
