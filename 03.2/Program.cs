
class Program
{
    static readonly Dictionary<char, int> priorities = new();

    static void Main()
    {
        int i = 1;
        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            priorities.Add(letter, i);
            i++;
        }
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            priorities.Add(letter, i);
            i++;
        }

        var prios = new List<int>();
        var lines = File.ReadLines("in.txt").ToArray();

        i = 0;
        while (i < lines.Length)
        {
            string a = lines[i];
            string b = lines[i + 1];
            string c = lines[i + 2];

            var prio = a.Intersect(b.Intersect(c));
            prios.Add(Program.priorities[prio.Single()]);

            i += 3;
        }

        var x = prios.Sum();

        Console.WriteLine(x);
        Console.ReadKey();
    }
}
