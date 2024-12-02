namespace AdventOfCode.Days;

public sealed class Day02 : BaseDay
{
    private readonly string _input;

    private readonly int[] _possible = [1, 2, 3];

    private Direction? _direction;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var report = _input.Lines().Select(line =>
                line.Split(SSpace, StringSplitOptions.RemoveEmptyEntries).ToInt().ToList())
            .ToList();

        var result = report.Select(level =>
        {
            Direction? direction = null;

            return level.SlidingWindow(2).All(window =>
            {
                int[] ints = window.ToArray();
                int firstValue = ints[0];
                int secondValue = ints[1];

                int diff = firstValue - secondValue;

                switch (Math.Sign(diff))
                {
                    case 1:
                        switch (direction)
                        {
                            case Direction.Increasing:
                                return false;
                            case Direction.Decreasing:
                                break;
                            case null:
                                direction = Direction.Decreasing;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        if (!_possible.Contains(Math.Abs(diff)))
                            return false;
                        break;
                    case -1:
                        switch (direction)
                        {
                            case Direction.Decreasing:
                                return false;
                            case Direction.Increasing:
                                break;
                            case null:
                                direction = Direction.Increasing;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        if (!_possible.Contains(Math.Abs(diff)))
                            return false;
                        break;
                    case 0:
                        return false;
                }

                return true;
            });
        }).ToList();

        return new ValueTask<string>(result.Count(level => level).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var report = _input.Lines().Select(line =>
                line.Split(SSpace, StringSplitOptions.RemoveEmptyEntries).ToInt().ToList())
            .ToList();

        var result = report.Select(line =>
        {
            _direction = null;

            if (IsSafeLine(line))
            {
                return true;
            }

            for (int i = 0; i < line.Count; i++)
            {
                _direction = null;

                var modifiedLine = new List<int>(line);
                modifiedLine.RemoveAt(i);
                if (IsSafeLine(modifiedLine))
                {
                    return true;
                }
            }

            return false;
        }).ToList();


        return new ValueTask<string>(result.Count(level => level).ToString());
    }

    private bool IsSafeLine(List<int> line)
    {
        return line.SlidingWindow(2).All(window =>
        {
            int[] ints = window.ToArray();
            int firstValue = ints[0];
            int secondValue = ints[1];

            int diff = firstValue - secondValue;

            switch (Math.Sign(diff))
            {
                case 1:
                    switch (_direction)
                    {
                        case Direction.Increasing:
                            return false;
                        case Direction.Decreasing:
                            break;
                        case null:
                            _direction = Direction.Decreasing;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (!_possible.Contains(Math.Abs(diff)))
                        return false;
                    break;
                case -1:
                    switch (_direction)
                    {
                        case Direction.Decreasing:
                            return false;
                        case Direction.Increasing:
                            break;
                        case null:
                            _direction = Direction.Increasing;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (!_possible.Contains(Math.Abs(diff)))
                        return false;
                    break;
                case 0:
                    return false;
            }

            return true;
        });
    }
}

internal enum Direction
{
    Increasing,
    Decreasing,
}
