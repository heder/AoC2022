class Program
{
    static char[,] world = new char[1000, 1000];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                world[x, y] = '.';
            }
        }

        foreach (var item in lines)
        {
            var coords = item.Split("->").Select(f => f.Trim()).ToArray();

            for (int i = 0; i < coords.Length - 1; i++)
            {
                var from = coords[i].Split(",").Select(int.Parse).ToArray();
                var to = coords[i + 1].Split(",").Select(int.Parse).ToArray();

                for (int x = Math.Min(from[0], to[0]); x <= Math.Max(from[0], to[0]); x++)
                {
                    for (int y = Math.Min(from[1], to[1]); y <= Math.Max(from[1], to[1]); y++)
                    {
                        world[x, y] = '#';
                    }
                }
            }
        }

        DumpWorld(490, 510, 0, 20);

        int highestY = int.MinValue;

        // Find lowest '#'
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                if (world[x, y] == '#')
                {
                    highestY = Math.Max(highestY, y);
                    break;
                }
            }
        }

        int floorY = highestY + 2;

        for (int x = 0; x < 1000; x++)
        {
            world[x, floorY] = '#';
        }

        int sandCount = 0;
        Position sandPos = new Position();
        bool atRest = true;
        while (true)
        {
            if (atRest == true)
            {
                sandPos = new Position() { X = 500, Y = 0 };
            }

            // Tomt under
            if (world[sandPos.X, sandPos.Y + 1] == '.')
            {
                sandPos.Y++; // d
                atRest = false;
            }
            // Block under, ledigt snett vänster neråt.
            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
                //world[sandPos.X - 1, sandPos.Y] == '.' &&
                world[sandPos.X - 1, sandPos.Y + 1] == '.')
            {
                sandPos.Y++;
                sandPos.X--;
                atRest = false;
            }
            // Block under, ledigt snett höger neråt.
            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
                //world[sandPos.X + 1, sandPos.Y] == '.' &&
                world[sandPos.X + 1, sandPos.Y + 1] == '.')
            {
                sandPos.Y++;
                sandPos.X++;
                atRest = false;
            }
            else
            {
                world[sandPos.X, sandPos.Y] = 'o';
                atRest = true;
                sandCount++;
            }

            //DumpWorld(490, 510, 0, 20);

            if (atRest == true && sandPos.X == 500 && sandPos.Y == 0)
            {
                Console.WriteLine(sandCount);
                Console.ReadKey();
            }
        }
    }

    internal class Position
    {
        public int X;
        public int Y;
    }

    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    {
        for (int y = ymin; y < ymax; y++)
        {
            for (int x = xmin; x < xmax; x++)
            {
                Console.Write(world[x, y] == ' ' ? '.' : world[x, y]);
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }
}