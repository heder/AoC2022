using System.Collections;
using System.Numerics;

class Program
{

    //static long debugVal;

    static Dictionary<long, Monkey> monkeys = new Dictionary<long, Monkey>();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        long i = 0;
        while (i < lines.Length)
        {

            Monkey m = new Monkey();

            long monkey = Convert.ToInt32(lines[i].Split(" ")[1].Trim(':'));
            i++;
            var items = lines[i].Split(":")[1].Split(",").Select(f => long.Parse(f)).ToList();
            i++;
            var operation = lines[i].Split(":")[1].Split("=")[1].Trim().Split(" ");
            i++;
            var test = Convert.ToInt32(lines[i].Split(":")[1].Trim().Split(" ")[2]);
            i++;
            var trueDest = Convert.ToInt32(lines[i].Split(':')[1].Trim().Split(" ")[3]);
            i++;
            var falseDest = Convert.ToInt32(lines[i].Split(':')[1].Trim().Split(" ")[3]);
            i++;
            i++;

            m.Items = items;
            m.Operation = operation[1];
            m.OperandA = operation[0];
            m.OperandB = operation[2];
            m.DivisibleBy = test;
            m.DestinationIfTrue = trueDest;
            m.DestinationIfFalse = falseDest;

            monkeys.Add(monkey, m);
        }

        var d = monkeys.Values.Aggregate(1, (c, m) => c * (int)m.DivisibleBy);

        for (long x = 1; x <= 10000; x++)
        {
            foreach (var m in monkeys)
            {
                var monkey = m.Value;

                // Inspect and increase worry level
                monkey.Inspections += monkey.Items.Count();
                var newList = new List<long>();
                foreach (var item in monkey.Items)
                {
                    long b;

                    if (monkey.OperandB == "old")
                        b = item;
                    else
                        b = Convert.ToInt32(monkey.OperandB);

                    long newVal = 0;
                    switch (monkey.Operation)
                    {
                        case "+":
                            newVal = item + b;
                            break;

                        case "*":
                            newVal = item * b;
                            break;

                        default:
                            break;
                    }

                    // Divide worry level
                    //newVal = newVal /= 3;
                    newVal %= d;

                    if (newVal % monkey.DivisibleBy == 0)
                    {
                        monkeys[monkey.DestinationIfTrue].Items.Add(newVal);
                    }
                    else
                    {
                        monkeys[monkey.DestinationIfFalse].Items.Add(newVal);
                    }
                }
                monkey.Items.Clear();



            }

            Console.WriteLine(x);
            if (x % 100 == 0)
            {
                DumpInspections(x);
            }
            //DumpMonkeys();
        }

        var top2 = monkeys.Values.OrderByDescending(f => f.Inspections).Take(2).ToArray();


        Console.WriteLine(top2[0].Inspections * top2[1].Inspections);
        Console.ReadKey();
    }

    private static void DumpInspections(long x)
    {
        Console.WriteLine($"---------");
        Console.WriteLine($"Round {x}");
        foreach (var item in monkeys)
        {
            Console.WriteLine($"{item.Key}: {item.Value.Inspections}");
        }
    }

    private static void DumpMonkeys()
    {
        foreach (var item in monkeys)
        {
            Console.WriteLine($"{item.Key}: {string.Join(", ", item.Value.Items.Select(f => f.ToString()))}");
        }
    }



    class Monkey
    {
        public List<long> Items { get; set; }

        public string Operation { get; set; }

        public long DivisibleBy { get; set; }

        public long DestinationIfTrue { get; set; }
        public long DestinationIfFalse { get; set; }
        public string OperandA { get; internal set; }
        public string OperandB { get; internal set; }

        public long Inspections { get; set; }
    }
}
