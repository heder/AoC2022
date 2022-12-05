class Program
{
    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();

        int stackrow = -1;
        int stackCount = -1;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().StartsWith('1'))
            {
                stackrow = i;
                stackCount = lines[i].Trim().Split("  ").Select(i => int.Parse(i)).Max();
            }
        }

        Stack<char>[] stacks = new Stack<char>[stackCount];
        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<char>();
        }

        for (int i = stackrow - 1; i >= 0; i--)
        {
            var row = lines[i];

            int stackOrdinal = 0;
            for (int j = 1; j < lines[stackrow].Length; j += 4)
            {
                if (row[j] != ' ')
                {
                    stacks[stackOrdinal].Push(row[j]);
                }

                stackOrdinal++;
            }
        }

        for (int i = stackrow + 2; i < lines.Length; i++)
        {
            var a = lines[i].Substring(5).Split("from");
            int c = int.Parse(a[0]);

            var b = a[1].Split("to");
            var from = int.Parse(b[0]) - 1;
            var to = int.Parse(b[1]) - 1;

            List<char> list = new List<char>();
            for (int j = 0; j < c; j++)
            {
                list.Add(stacks[from].Pop());
            }

            list.Reverse();
            foreach (var item in list)
            {
                stacks[to].Push(item);
            }
        }

        string result = "";
        for (int i = 0; i < stacks.Length; i++)
        {
            result += stacks[i].Peek();
        }

        Console.WriteLine(result);
        Console.ReadKey();
    }
}
