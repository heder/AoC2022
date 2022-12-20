class Node
{
    public Node next;
    public Node prev;

    public long value;
}


class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("in.txt").Select(int.Parse);

        var originalOrder = new List<Node>();

        Node first = null;
        Node prev = null;
        Node theZero = null;

        int i = 1;
        foreach (var item in lines)
        {
            Node n = new Node();
            first ??= n;

            if (item == 0)
            {
                theZero = n;
            }

            n.value = item * (long)811589153;
            n.prev = prev;

            if (prev != null)
            {
                prev.next = n;
            }

            prev = n;

            if (i == lines.Count())
            {
                n.next = first;
                first.prev = n;
            }

            originalOrder.Add(n);

            i++;
        }

        for (int j = 0; j < 10; j++)
        {
            foreach (var item in originalOrder)
            {
                long r = item.value % 4999;

                if (r != 0)
                {
                    Node newPos = item;

                    Node oldF = item.next;
                    Node oldB = item.prev;

                    oldF.prev = oldB;
                    oldB.next = oldF;

                    if (r > 0)
                    {
                        for (int f = 0; f <= r; f++)
                        {
                            newPos = newPos.next;
                        }
                    }
                    else if (r < 0)
                    {
                        for (int b = 0; b < Math.Abs(r); b++)
                        {
                            newPos = newPos.prev;
                        }
                    }

                    item.next = newPos;
                    item.prev = newPos.prev;

                    newPos.prev = item;
                    item.prev.next = item;
                }
            }
        }

        long s = 0;
        for (int z = 1; z < 3500; z++)
        {
            theZero = theZero.next;

            if (z == 1000) s += theZero.value;
            if (z == 2000) s += theZero.value;
            if (z == 3000) s += theZero.value;
        }

        Console.WriteLine(s);
        Console.ReadKey();
    }
}
