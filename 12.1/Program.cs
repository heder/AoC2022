using System.Collections;

class Program
{

    //static int debugVal;

    static Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int i = 0;
        while (i < lines.Length)
        {

            Monkey m = new Monkey();

            int monkey = Convert.ToInt32(lines[i].Split(" ")[1].Trim(':'));
            i++;
            var items = lines[i].Split(":")[1].Split(",").Select(f => int.Parse(f)).ToList();
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



        for (int x = 0; x < 20; x++)
        {
            foreach (var m in monkeys)
            {
                var monkey = m.Value;

                // Inspect and increase worry level
                monkey.Inspections += monkey.Items.Count();
                var newList = new List<int>();
                foreach (var item in monkey.Items)
                {
                    int b;

                    if (monkey.OperandB == "old")
                        b = item;
                    else
                        b = Convert.ToInt32(monkey.OperandB);

                    int newVal = 0;
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
                    newVal = newVal /= 3;

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

            
            DumpMonkeys();
        }

        var top2 = monkeys.Values.OrderByDescending(f => f.Inspections).Take(2).ToArray();


        Console.WriteLine(top2[0].Inspections * top2[1].Inspections);
        Console.ReadKey();
    }

    private static void DumpMonkeys()
    {
        foreach (var item in monkeys)
        {
            Console.WriteLine($"{item.Key}: {string.Join(", ",  item.Value.Items.Select(f => f.ToString()))}");
        }
    }

    class Monkey
{
    public List<int> Items { get; set; }

    public string Operation { get; set; }

    public int DivisibleBy { get; set; }

    public int DestinationIfTrue { get; set; }
    public int DestinationIfFalse { get; set; }
    public string OperandA { get; internal set; }
    public string OperandB { get; internal set; }

        public int Inspections { get; set; }
    }


}
