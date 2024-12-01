namespace AdventOfCode.Extensions;

public static class MathExtensions
{
    public static int Gcd(this int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int Lcm(this int a, int b)
    {
        return a / a.Gcd(b) * b;
    }


}
