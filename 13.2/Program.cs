class Program
{
    static void Main()
    {
        var l = File.ReadAllLines("in.txt").ToList();
        l = l.Where(f => f.Length > 0).ToList();

        l.Add("[[2]]");
        l.Add("[[6]]");

        string[] lines = l.ToArray();

        List<PacketList> packets = new List<PacketList>();

        int i = 0;
        while (i < lines.Length)
        {
            var res1 = ParsePacket(lines[i]);
            res1.Packetstring = lines[i];
            packets.Add(res1);
            i++;
            var res2 = ParsePacket(lines[i]);
            res2.Packetstring = lines[i];
            packets.Add(res2);
            i++;
        }

        var p = packets.ToArray();

        int moved = 1;
        while (moved > 0)
        {
            moved = 0;
            for (int c = 0; c < p.Length - 1; c++)
            {
                if (CompareList(p[c], p[c + 1]) == false)
                {
                    var temp = p[c];
                    p[c] = p[c + 1];
                    p[c + 1] = temp;
                    moved++;
                    break;
                }
            }
        }


        int a = 1;
        foreach (var item in p)
        {
            if (item.Packetstring == "[[2]]") break;
            a++;
        }

        int b = 1;
        foreach (var item in p)
        {
            if (item.Packetstring == "[[6]]") break;
            b++;
        }


        Console.WriteLine(a * b);
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

    public string Packetstring { get; set; }

    public List<PacketData> List { get; set; } = new List<PacketData>();
}
