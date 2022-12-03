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

        var lines = File.ReadLines("in.txt").ToArray();

        var rucksacks = new List<Rucksack>();
        foreach (var item in lines)
        {
            rucksacks.Add(new Rucksack(item));
        }

        var x = rucksacks.Sum(f => f.Prio);

        Console.WriteLine(x);
        Console.ReadKey();
    }

    class Rucksack
    {
        public Rucksack(string content)
        {
            string a = content[..(content.Length / 2)];
            string b = content[(content.Length / 2)..];

            var common = a.Intersect(b);
            Prio = Program.priorities[common.Single()];
        }

        public int Prio { get; set; }
    }
}
