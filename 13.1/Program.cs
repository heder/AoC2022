class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        List<int> c = new List<int>();
        int pair = 1;
        int i = 0;
        while (i < lines.Length)
        {
            var res1 = ParsePacket(lines[i]);
            i++;
            var res2 = ParsePacket(lines[i]);
            i++;
            i++;

            var result = CompareList(res1, res2);

            if (result ?? false)
            {
                c.Add(pair);
            }

            pair++;
        }

        Console.WriteLine(c.Sum());
        Console.ReadKey();
    }

    private static bool? CompareList(PacketList res1, PacketList res2)
    {
        for (int i = 0; i < Math.Min(res1.List.Count, res2.List.Count); i++)
        {
            if (res1.List[i] is PacketValue left && res2.List[i] is PacketValue right)
            {
                if (left.Value < right.Value)
                {
                    return true;
                }
                else if (left?.Value > right?.Value)
                {
                    return false;
                }
            }

            if (res1.List[i] is PacketList l1 && res2.List[i] is PacketList l2)
            {
                var res = CompareList(l1, l2);
                if (res != null)
                {
                    return res;
                }
            }

            if (res1.List[i] is PacketValue pv1 && res2.List[i] is PacketList pl1)
            {
                var newList = new PacketList(res1.Parent);
                var newData = new PacketValue(newList, pv1.Value);
                newList.List.Add(newData);
                res1.List[i] = newList;

                return CompareList(newList, pl1);
            }

            if (res1.List[i] is PacketList pl2 && res2.List[i] is PacketValue pv2)
            {
                var newList = new PacketList(res2.Parent);
                var newData = new PacketValue(newList, pv2.Value);
                newList.List.Add(newData);
                res2.List[i] = newList;

                return CompareList(pl2, newList);
            }
        }

        if (res1.List.Count < res2.List.Count)
        {
            return true;
        }

        if (res1.List.Count > res2.List.Count)
        {
            return false;
        }

        return null;
    }


    private static PacketList ParsePacket(string v)
    {
        PacketList root = new PacketList(null);
        PacketList current = root;

        v = v[1..^1];

        for (int i = 0; i < v.Length; i++)
        {
            if (v[i] == '[')
            {
                var n = new PacketList(current);
                current.List.Add(n);
                current = n;
            }
            else if (v[i] == ']')
            {
                current = current.Parent;
            }
            else if (v[i] == ',')
            {
            }
            else
            {
                int n;
                bool result;
                if (v[i] == '1' && i < v.Length - 1 && v[i + 1] == '0')
                {
                    n = 10;
                    result = true;
                }
                else
                {
                    result = int.TryParse(v[i].ToString(), out n);
                }

                if (result == true)
                { 
                    current.List.Add(new PacketValue(current, n));
                }
            }
        }

        return root;
    }
}

internal abstract class PacketData
{
    public PacketData(PacketList parent)
    {
        Parent = parent;
    }

    public PacketList Parent { get; set; } = null;
}

internal class PacketValue : PacketData
{
    public PacketValue(PacketList parent, int value) : base(parent)
    {
        Value = value;
    }

    public int Value { get; set; }
}

internal class PacketList : PacketData
{
    public PacketList(PacketList parent) : base(parent) { }

    public List<PacketData> List { get; set; } = new List<PacketData>();
}
