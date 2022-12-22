class Monkey
{
    public string name;

    public decimal oresult;
    public bool ocalculated;

    public decimal result;
    public bool calculated = false;

    public char op;

    public Monkey val1;
    public Monkey val2;

    public void Calc()
    {
        if (val1.calculated && val2.calculated)
        {
            switch (op)
            {
                case '+':
                    result = val1.result + val2.result;
                    break;

                case '-':
                    result = val1.result - val2.result;
                    break;

                case '*':
                    result = val1.result * val2.result;
                    break;

                case '/':
                    result = val1.result / val2.result;
                    break;

                default:
                    break;
            }

            calculated = true;
        }
    }
}


class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("in.txt");

        var monkeys = new Dictionary<string, Monkey>();

        foreach (var item in lines)
        {
            var s1 = item.Split(':');
            var name = s1[0];

            Monkey m;
            if (monkeys.ContainsKey(name))
            {
                m = monkeys[name];
            }
            else
            {
                m = new Monkey() { name = name };
                monkeys.Add(name, m);
            }

            var s2 = s1[1].Trim().Split(' ');
            if (s2.Length == 1)
            {
                m.result = Convert.ToInt32(s2[0]);
                m.oresult = m.result;
                m.calculated = true;
                m.ocalculated = m.calculated;
            }
            else
            {
                var name1 = s2[0];
                var name2 = s2[2];

                Monkey mo1;
                if (monkeys.ContainsKey(name1))
                {
                    mo1 = monkeys[name1];
                }
                else
                {
                    mo1 = new Monkey() { name = name1 };
                    monkeys.Add(name1, mo1);
                }

                Monkey mo2;
                if (monkeys.ContainsKey(name2))
                {
                    mo2 = monkeys[name2];
                }
                else
                {
                    mo2 = new Monkey() { name = name2 };
                    monkeys.Add(name2, mo2);
                }

                m.val1 = mo1;
                m.val2 = mo2;

                m.op = s2[1][0];
            }
        }

        var r = monkeys["root"];
        r.val2.result = 32310041242752M;
        r.val2.calculated= true;

        var human = monkeys["humn"];

        while (true)
        {
            // Manual divide & conquer gessing :-)
            string guess = Console.ReadLine();            
            human.result = Convert.ToDecimal(guess);

            Calc(r);
            if (r.val1.result == r.val2.result)
            {
                Console.ReadKey();
            }

            Console.WriteLine(r.val1.result - r.val2.result);

            foreach (var item in monkeys)
            {
                item.Value.result = item.Value.oresult;
                item.Value.calculated = item.Value.ocalculated;
            }

        }


    }


    private static void Calc(Monkey r)
    {
        if (r.val1.calculated == false)
            Calc(r.val1);

        if (r.val2.calculated == false)
            Calc(r.val2);

        if (r.val1.calculated == true && r.val2.calculated == true)
        {
            r.Calc();
        }
    }
}
