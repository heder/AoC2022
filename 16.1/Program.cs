

public class Valve
{
    public string Name { get; set; }
    public List<Valve> Destinations { get; set; } = new List<Valve>();
    public int Rate { get; set; }

    public bool IsOpen { get; set; }
}


public class Location
{
    public Valve At { get; set; }
    //public bool Open { get; set; }
    //public string MoveTo { get; set; }
}


class Program
{

    static Dictionary<string, Valve> valves = new Dictionary<string, Valve>();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt");

        foreach (var item in lines)
        {
            string vv = item.Replace("valve ", "valves ");

            string name = vv[6..8];
            Valve v;

            if (valves.ContainsKey(name))
            {
                v = valves[name];
            }
            else
            {
                v = new Valve();
                v.Name = name;
                valves.Add(v.Name, v);
            }

            var s1 = vv.Split(";");
            var s2 = s1[0].Split("=");

            v.Rate = Convert.ToInt32(s2[1]);

            var s3 = s1[1].Split("valves");
            var s4 = s3[1].Split(",").Select(f => f.Trim());

            foreach (var dest in s4)
            {
                if (valves.ContainsKey(dest) == false)
                {
                    var d = new Valve() { Name = dest };
                    valves.Add(d.Name, d);
                    v.Destinations.Add(d);
                }
                else
                {
                    v.Destinations.Add(valves[dest]);
                }
            }
        }

        valves["AA"].IsOpen = true;

        Dictionary<int, List<Location>> possibleActions = new Dictionary<int, List<Location>>();

        possibleActions.Add(0, new List<Location>() { new Location() { At = valves["AA"] } });
        for (int minute = 0; minute < 30; minute++)
        {
            var locations = possibleActions[minute];

            List<Location> actions = new List<Location>();
            foreach (var l in locations)
            {
                if (l.At.IsOpen == false)
                {
                    // Open valve. Possible movements will occur next minute.
                    // We are still at current valve
                    l.At.IsOpen = true;
                    actions.Add(new Location() { At = valves[l.At.Name] });
                }
                else
                {
                    // We did not open valve. We can move to:...
                    foreach (var item in l.At.Destinations)
                    {
                        actions.Add(new Location() { At = valves[item.Name] });
                    }
                }
            }

            possibleActions.Add(minute + 1, actions);
        }
    }
}



//        for (int x = 0; x < 1000; x++)
//        {
//            for (int y = 0; y < 1000; y++)
//            {
//                world[x, y] = '.';
//            }
//        }

//        foreach (var item in lines)
//        {
//            var coords = item.Split("->").Select(f => f.Trim()).ToArray();

//            for (int i = 0; i < coords.Length - 1; i++)
//            {
//                var from = coords[i].Split(",").Select(int.Parse).ToArray();
//                var to = coords[i + 1].Split(",").Select(int.Parse).ToArray();

//                for (int x = Math.Min(from[0], to[0]); x <= Math.Max(from[0], to[0]); x++)
//                {
//                    for (int y = Math.Min(from[1], to[1]); y <= Math.Max(from[1], to[1]); y++)
//                    {
//                        world[x, y] = '#';
//                    }
//                }
//            }
//        }

//        DumpWorld(490, 510, 0, 20);

//        int highestY = int.MinValue;

//        // Find lowest '#'
//        for (int x = 0; x < 1000; x++)
//        {
//            for (int y = 0; y < 1000; y++)
//            {
//                if (world[x,y] == '#')
//                {
//                    highestY = Math.Max(highestY, y);
//                    break;
//                }
//            }
//        }

//        int sandCount = 0;
//        Position sandPos = new Position();
//        bool atRest = true;

//        while (true)
//        {
//            if (atRest == true)
//            {
//                sandPos = new Position() { X = 500, Y = 0 };
//            }

//            // Tomt under
//            if (world[sandPos.X, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++; // d
//                atRest = false;
//            }
//            // Block under, ledigt snett vänster neråt.
//            else if (world[sandPos.X, sandPos.Y + 1] != '.' && world[sandPos.X - 1, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++;
//                sandPos.X--;
//                atRest = false;
//            }
//            // Block under, ledigt snett höger neråt.
//            else if (world[sandPos.X, sandPos.Y + 1] != '.' && world[sandPos.X + 1, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++;
//                sandPos.X++;
//                atRest = false;
//            }
//            else
//            {
//                world[sandPos.X, sandPos.Y] = 'o';
//                atRest = true;
//                sandCount++;
//            }

//            DumpWorld(450, 550, 0, 20);

//            if (sandPos.Y > highestY)
//            {
//                Console.WriteLine(sandCount);
//                Console.ReadKey();
//            }
//        }
//    }

//    internal class Position
//    {
//        public int X;
//        public int Y;
//    }

//    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
//    {
//        for (int y = ymin; y < ymax; y++)
//        {
//            for (int x = xmin; x < xmax; x++)
//            {
//                Console.Write(world[x, y] == ' ' ? '.' : world[x, y]);
//            }

//            Console.Write(Environment.NewLine);
//        }

//        Console.Write(Environment.NewLine);
//    }
//}