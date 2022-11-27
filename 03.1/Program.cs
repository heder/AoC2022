class Program
{
    static void Main()
    {
        string[] numbers = File.ReadLines("in.txt").ToArray();

        var bits = numbers[0].Length;
        char[] gamma = new char[bits];
        char[] epsilon = new char[bits];

        for (int i = 0; i < bits; i++)
        {
            int no1 = numbers.Select(f => f[i]).Count(f => f == '1');
            int no0 = numbers.Select(f => f[i]).Count(f => f == '0');

            gamma[i] += (no1 > no0) ? '1' : '0';
            epsilon[i] += (no1 < no0) ? '1' : '0';
        }

        int g = Convert.ToInt32(new string(gamma), 2);
        int e = Convert.ToInt32(new string(epsilon), 2);

        Console.WriteLine(g * e);
        Console.ReadKey();
    }
}

