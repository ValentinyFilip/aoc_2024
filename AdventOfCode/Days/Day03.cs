using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public sealed class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var numbers = Regex.Matches(_input, @"mul\((\d+),(\d+)\)")
            .Select(match => (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)))
            .ToList();

        int result = numbers.Select(number => number.Item1 * number.Item2).Sum();

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        bool shouldMul = true;

        int result = Regex.Matches(_input, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
            .Select(match =>
            {
                if (match.Value.StartsWith("mul"))
                {
                    if (shouldMul)
                    {
                        return match.Groups[1].Value.StringToInt() * match.Groups[2].Value.StringToInt();
                    }
                }
                else
                    shouldMul = match.Value switch
                    {
                        "do()" => true,
                        "don't()" => false,
                        _ => shouldMul
                    };

                return 0;
            }).Sum();

        return new ValueTask<string>(result.ToString());
    }
}
