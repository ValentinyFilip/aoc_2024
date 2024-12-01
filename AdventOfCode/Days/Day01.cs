using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var numbers = _input.GetLines()
            .Select(line =>
                line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => new { Col1 = parts[0].StringToInt(), Col2 = parts[1].StringToInt() })
            .ToList();

        var list1 = numbers.Select(x => x.Col1).OrderBy(n => n).ToList();
        var list2 = numbers.Select(x => x.Col2).OrderBy(n => n).ToList();

        int result = list1.Zip(list2, (n1, n2) => Math.Abs(n1 - n2)).Sum();

        return ValueTask.FromResult(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var numbers = _input.GetLines()
            .Select(line =>
                line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries))
            .Select(parts => new { Col1 = parts[0].StringToInt(), Col2 = parts[1].StringToInt() })
            .ToList();

        var keys = numbers.Select(x => x.Col1).OrderBy(n => n).ToList();
        var values = numbers.Select(x => x.Col2).OrderBy(n => n).ToList();

        int result = keys.Select(x => x * values.Count(v => v == x)).Sum();

        return ValueTask.FromResult(result.ToString());
    }
}
