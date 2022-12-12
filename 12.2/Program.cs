class Program
{
    static int yMax;
    static int xMax;
    static Cell[,] world;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        world = new Cell[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y] = new Cell(lines[y][x], x, y);
            }
        }

        var start = world.Cast<Cell>().Where(f => f.Height == 'S').Single();
        start.Height = 'a';

        List<int> lengths= new List<int>();
        int i = 0;

        var startingPositions = world.Cast<Cell>().Where(f => f.Height == 'a').ToList();
        foreach (var item in startingPositions)
        {
            ResetWorld();
            item.Distance = 0;
            TraverseWorld(item);
            var e = world.Cast<Cell>().Where(f => f.Height == 'E').Single();
            lengths.Add(e.Distance);
            Console.WriteLine(i);
            i++;
        }

        Console.WriteLine(lengths.Min());
        Console.ReadKey();
    }

    private static void TraverseWorld(Cell c)
    {
        //DumpWorld(0, xMax, 0, yMax);

        if (c.Height == 'E')
        {
            return;
        }

        var paths = GetPaths(c);
        foreach (var item in paths)
        {
            item.Distance = c.Distance + 1;
            TraverseWorld(item);
        }

        return;
    }

    internal static List<Cell> GetPaths(Cell c)
    {
        var positions = new List<Cell>();

        if (c.Y > 0)
        {
            var u = world[c.X, c.Y - 1];

            if (CheckDistanceAndHeight(c, u))
            {
                positions.Add(u);
            }
        }

        if (c.Y < yMax - 1)
        {
            var d = world[c.X, c.Y + 1];

            if (CheckDistanceAndHeight(c, d))
            {
                positions.Add(d);
            }
        }

        if (c.X > 0)
        {
            var l = world[c.X - 1, c.Y];

            if (CheckDistanceAndHeight(c, l))
            {
                positions.Add(l);
            }
        }

        if (c.X < xMax - 1)
        {
            var r = world[c.X + 1, c.Y];

            if (CheckDistanceAndHeight(c, r))
            {
                positions.Add(r);
            }
        }

        return positions;
    }

    private static bool CheckDistanceAndHeight(Cell c, Cell d)
    {
        return c.Distance + 1 < d.Distance && ((d.Height <= c.Height + 1 && d.Height != 'E') || (c.Height == 'z' && d.Height == 'E') || (c.Height == 'S' && d.Height == 'a'));
    }


    private static void ResetWorld()
    {
        foreach (var item in world)
        {
            item.Height = item.OriginalHeight;
            item.Distance = int.MaxValue;
        }
    }

    internal class Cell
    {
        public Cell(char height, int x, int y)
        {
            Height = height;
            OriginalHeight = height;
            Distance = int.MaxValue;
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public char Height;
        public char OriginalHeight;

        public int Distance;
    }


    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    {
        for (int y = ymin; y < ymax; y++)
        {
            for (int x = xmin; x < xmax; x++)
            {
                Console.Write(world[x, y].Distance + " | ");
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }
}
