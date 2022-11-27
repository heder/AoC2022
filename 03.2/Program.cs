using System;

class Program
{
    static void Main()
    {
        string[] numbers = File.ReadLines("in.txt").ToArray();

        var bits = numbers[0].Length;

        string[] oxygen = new string[numbers.Length];
        string[] co2 = new string[numbers.Length];
        numbers.CopyTo(oxygen, 0);
        numbers.CopyTo(co2, 0);

        // oxygen
        for (int i = 0; i < bits; i++)
        {
            int no1 = oxygen.Select(f => f[i]).Count(f => f == '1');
            int no0 = oxygen.Select(f => f[i]).Count(f => f == '0');
            char mostCommon = (no1 >= no0) ? '1' : '0';

            oxygen = oxygen.Where(f => f[i] == mostCommon).ToArray();

            if (oxygen.Length == 1) break;
        }

        // co2
        for (int i = 0; i < bits; i++)
        {
            int no1 = co2.Select(f => f[i]).Count(f => f == '1');
            int no0 = co2.Select(f => f[i]).Count(f => f == '0');
            char leastCommon = (no1 < no0) ? '1' : '0';

            co2 = co2.Where(f => f[i] == leastCommon).ToArray();

            if (co2.Length == 1) break;
        }

        int o = Convert.ToInt32(new string(oxygen.First()), 2);
        int c = Convert.ToInt32(new string(co2.First()), 2);

        Console.WriteLine(o * c);
        Console.ReadKey();
    }
}

