class Monkey
{
    public string name;

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
                m.calculated = true;
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

        Calc(r);

        Console.WriteLine(r.result);
        Console.ReadKey();
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




    //    Node first = null;
    //    Node prev = null;
    //    Node theZero = null;

    //    int i = 1;
    //    foreach (var item in lines)
    //    {
    //        Node n = new Node();
    //        first ??= n;

    //        if (item == 0)
    //        {
    //            theZero = n;
    //        }

    //        n.value = item;
    //        n.prev = prev;

    //        if (prev != null)
    //        {
    //            prev.next = n;
    //        }

    //        prev = n;

    //        if (i == lines.Count())
    //        {
    //            n.next = first;
    //            first.prev = n;
    //        }

    //        originalOrder.Add(n);

    //        i++;
    //    }

    //    foreach (var item in originalOrder)
    //    {

    //        if (item.value != 0)
    //        {
    //            Node newPos = item;

    //            Node oldF = item.next;
    //            Node oldB = item.prev;

    //            oldF.prev = oldB;
    //            oldB.next = oldF;

    //            if (item.value > 0)
    //            {
    //                for (int f = 0; f <= item.value; f++)
    //                {
    //                    newPos = newPos.next;
    //                }
    //            }
    //            else if (item.value < 0)
    //            {
    //                for (int b = 0; b < Math.Abs(item.value); b++)
    //                {
    //                    newPos = newPos.prev;
    //                }
    //            }

    //            item.next = newPos;
    //            item.prev = newPos.prev;

    //            newPos.prev = item;
    //            item.prev.next = item;
    //        }
    //    }

    //    int s = 0;
    //    for (int z = 1; z < 3500; z++)
    //    {
    //        theZero = theZero.next;

    //        if (z == 1000) s += theZero.value;
    //        if (z == 2000) s += theZero.value;
    //        if (z == 3000) s += theZero.value;
    //    }

    //    Console.WriteLine(s);
    //    Console.ReadKey();
    //}
}
