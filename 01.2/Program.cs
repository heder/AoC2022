class Program
{
    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();
        var groups = new List<int>();

        int curr = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "")
            {
                groups.Add(curr);
                curr = 0;
                continue;
            }

            curr += Convert.ToInt32(lines[i]);
        }

        groups.Add(curr);

        var x = groups.OrderByDescending(f => f).Take(3).Sum();

        Console.WriteLine(x);
        Console.ReadKey();
    }
}

