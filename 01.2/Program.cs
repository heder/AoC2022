class Program
{
    static void Main()
    {
        int[] lines = File.ReadLines("in.txt").Select(f => Convert.ToInt32(f)).ToArray();
        
        List<int> groups = new();
        int currentPos = 0;
        
        while (currentPos < lines.Length - 2)
        {
            groups.Add(lines[currentPos] + lines[currentPos + 1] + lines[currentPos + 2]);
            currentPos++;
        }

        int? prev = null;
        int noIncreased = 0;
        var groupsArray = groups.ToArray();

        for (int i = 0; i < groupsArray.Length; i++)
        {
            if (groupsArray[i] > prev) noIncreased++;
            prev = groupsArray[i];
        }

        Console.WriteLine(noIncreased);
        Console.ReadKey();
    }
}

