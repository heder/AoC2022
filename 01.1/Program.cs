class Program
{
    static void Main()
    {
        int[] lines = File.ReadLines("in.txt").Select(f => Convert.ToInt32(f)).ToArray();

        int? prev = null;
        int noIncreased = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] > prev) noIncreased++;
            prev = lines[i];
        }

        Console.WriteLine(noIncreased);
        Console.ReadKey();
    }
}

