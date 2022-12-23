internal class Cell
{
    public Cell(char type, int x, int y)
    {
        T = type;
        X = x;
        Y = y;
    }

    public int X;
    public int Y;

    public bool hasproposedposition = false;

    public int pX;
    public int pY;

    public char T;
}

class Program
{
    static bool dump = true;

    static Cell[,] world;   
    static int currentHeading = 0;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        world = new Cell[1000, 1000];

        for (int y = 0; y < 1000; y++)
        {
            for (int x = 0; x < 1000; x++)
            {
                world[x, y] = new Cell('.', x, y);
            }
        }

        int initx = lines[0].Length;
        int inity = lines.Length;

        for (int y = 0; y < inity; y++)
        {
            for (int x = 0; x < initx; x++)
            {
                world[500 + x, 500 + y].T = lines[y][x];
            }
        }

        var elves = world.Cast<Cell>().Where(f => f.T == '#').ToList();

       // DumpWorld(490, 580, 480, 580);

        for (int i = 1; i < 200000000; i++)
        
        {
            int no = 0;

            foreach (var item in elves)
            {
                item.hasproposedposition = false;

                if (HasNeighbour(item.X, item.Y) == false)
                {
                    // Do nothing
                    no++;
                    if (no == elves.Count())
                    {
                        Console.ReadKey();
                    }
                }
                else
                {
                    List<int> list = new List<int>();
                    if (currentHeading == 0) { list = new List<int>() { 0, 1, 2, 3 }; };
                    if (currentHeading == 1) { list = new List<int>() { 1, 2, 3, 0 }; };
                    if (currentHeading == 2) { list = new List<int>() { 2, 3, 0, 1 }; };
                    if (currentHeading == 3) { list = new List<int>() { 3, 0, 1, 2 }; };

                    foreach (var h in list)
                    {
                        if (h == 0)
                        {
                            if (HasNorthNeighbour(item.X, item.Y) == false)
                            {
                                item.pX = item.X;
                                item.pY = item.Y - 1;
                                item.hasproposedposition = true;
                                break;
                            }
                        }

                        if (h == 1)
                        {
                            if (HasSouthNeighbour(item.X, item.Y) == false)
                            {
                                item.pX = item.X;
                                item.pY = item.Y + 1;
                                item.hasproposedposition = true;
                                break;
                            }
                        }

                        if (h == 2)
                        {
                            if (HasWestNeighbour(item.X, item.Y) == false)
                            {
                                item.pX = item.X - 1;
                                item.pY = item.Y;
                                item.hasproposedposition = true;
                                break;
                            }
                        }

                        if (h == 3)
                        {
                            if (HasEastNeighbour(item.X, item.Y) == false)
                            {
                                item.pX = item.X + 1;
                                item.pY = item.Y;
                                item.hasproposedposition = true;
                                break;
                            }
                        }
                    }
                }
            }

            var proposedelves = elves.Where(f => f.hasproposedposition).ToList();
            var z = proposedelves.GroupBy(f => new { f.pX, f.pY }).Where(f => f.Count() > 1).ToList();

            foreach (var elf in proposedelves)
            {
                if (z.Any(f => f.Key.pX == elf.pX && f.Key.pY == elf.pY))
                {
                    continue;
                }

                var cell = world[elf.pX, elf.pY];

                world[elf.pX, elf.pY] = elf;

                world[elf.X, elf.Y] = cell;
                cell.X = elf.X; cell.Y = elf.Y;

                elf.X = elf.pX;
                elf.Y = elf.pY;

                elf.pY = -1;
                elf.pX = -1;
                elf.hasproposedposition = false;
            }

           // DumpWorld(490, 580, 480, 580);

            currentHeading++;
            if (currentHeading == 4)
            {
                currentHeading = 0;
            }
        }

        var xmin = elves.Min(f => f.X);
        var xmax = elves.Max(f => f.X);

        var ymin = elves.Min(f => f.Y);
        var ymax = elves.Max(f => f.Y);

        int c = 0;

        for (int y = ymin; y <= ymax; y++)
        {
            for (int x = xmin; x <= xmax; x++)
            {
                if (world[x, y].T == '.') c++;
            }
        }

        Console.WriteLine(c);
        Console.ReadKey();
    }

    private static bool HasNeighbour(int x, int y)
    {
        return (world[x - 1, y].T == '#' ||
                world[x - 1, y - 1].T == '#' ||
                world[x, y - 1].T == '#' ||
                world[x + 1, y - 1].T == '#' ||
                world[x + 1, y].T == '#' ||
                world[x + 1, y + 1].T == '#' ||
                world[x, y + 1].T == '#' ||
                world[x - 1, y + 1].T == '#');
    }

    private static bool HasNorthNeighbour(int x, int y)
    {
        return (world[x - 1, y - 1].T == '#' ||
                world[x, y - 1].T == '#' ||
                world[x + 1, y - 1].T == '#');
    }

    private static bool HasSouthNeighbour(int x, int y)
    {
        return (world[x - 1, y + 1].T == '#' ||
                world[x, y + 1].T == '#' ||
                world[x + 1, y + 1].T == '#');
    }

    private static bool HasWestNeighbour(int x, int y)
    {
        return (world[x - 1, y - 1].T == '#' ||
                world[x - 1, y].T == '#' ||
                world[x - 1, y + 1].T == '#');
    }

    private static bool HasEastNeighbour(int x, int y)
    {
        return (world[x + 1, y - 1].T == '#' ||
                world[x + 1, y].T == '#' ||
                world[x + 1, y + 1].T == '#');
    }


    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    {
        if (dump == false) return;

        for (int y = ymin; y < ymax; y++)
        {
            for (int x = xmin; x < xmax; x++)
            {
                Console.Write(world[x, y].T);
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }
}
