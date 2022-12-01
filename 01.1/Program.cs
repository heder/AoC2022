class Program
{
    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();

        int highest = 0;
        int curr = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "")
            {
                if (curr > highest)
                {
                    highest = curr;
                }

                curr = 0;
                continue;
            }

            curr += Convert.ToInt32(lines[i]);
        }

        if (curr > highest)
        {
            highest = curr;
        }

        Console.WriteLine(highest);
        Console.ReadKey();
    }
}

