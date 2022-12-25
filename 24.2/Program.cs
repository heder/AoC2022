internal class Blizzard
{
    public Blizzard(char type, int x, int y)
    {
        T = type;
        X = x;
        Y = y;
    }

    public int X;
    public int Y;

    public char T;
}

class Pos
{
    public Pos(int X, int Y)
    {
        x = X;
        y = Y;
    }

    public int x = 0;
    public int y = 0;
}

class Program
{
    static int yMax;
    static int xMax;
    static int[,] world;

    static List<Blizzard> blizzards = new List<Blizzard>();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;

        world = new int[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (lines[y][x] == '#')
                    world[x, y] = 9;
                else
                    world[x, y] = 0;

                if (new List<char>() { '>', '<', '^', 'v' }.Contains(lines[y][x]))
                {
                    world[x, y]++;
                    blizzards.Add(new Blizzard(lines[y][x], x, y));
                }
            }
        }

        Pos startpos = new Pos(0, 0);
        Pos endPos = new Pos(0, 0); ;

        //DumpWorld(0, xMax, 0, yMax);

        for (int i = 0; i < xMax; i++)
        {
            if (lines[0][i] == '.')
            {
                startpos = new Pos(i, 0);
                world[i, 0] = 0;
            }

            if (lines[yMax - 1][i] == '.')
            {
                endPos = new Pos(i, yMax - 1);
                world[i, 0] = 0;
            }
        }

        DumpWorld(0, xMax, 0, yMax);

        FindPath(startpos, endPos);
        FindPath(endPos, startpos);
        FindPath(startpos, endPos);

        Console.ReadKey();

        static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
        {
            return;

            for (int y = ymin; y < ymax; y++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    Console.Write(world[x, y]);
                }

                Console.Write(Environment.NewLine);
            }

            Console.Write(Environment.NewLine);
        }

        void FindPath(Pos startPos, Pos endPos)
        {
            List<int> distances = new List<int>();
            var moves = new Dictionary<int, List<Pos>>();
            moves.Add(0, new List<Pos>() { new Pos(startPos.x, startPos.y) });

            for (int minute = 0; minute < 10000; minute++)
            {
                MoveBlizzards();

                var tomove = moves[minute];

                DumpWorld(0, xMax, 0, yMax);

                List<Pos> list = new List<Pos>();

                foreach (var item in tomove)
                {
                    var vm = ValidMoves(item.x, item.y);

                    foreach (var m in vm)
                    {
                        if (m.x == endPos.x && m.y == endPos.y)
                        {
                            Console.WriteLine(minute + 1);
                            return;
                        }
                        else
                        {
                            list.Add(new Pos(m.x, m.y));
                        }
                    }
                }

                moves.Add(minute + 1, list.DistinctBy(f => new { f.x, f.y }).ToList());
            }
        }

        List<Pos> ValidMoves(int x, int y)
        {
            List<Pos> ret = new List<Pos>();
            if (world[x, y] == 0) ret.Add(new Pos(x, y));
            if (world[x - 1, y] == 0) ret.Add(new Pos(x - 1, y));
            if (world[x + 1, y] == 0) ret.Add(new Pos(x + 1, y));
            if (y != yMax - 1 && world[x, y + 1] == 0) ret.Add(new Pos(x, y + 1));
            if (y != 0 && world[x, y - 1] == 0) ret.Add(new Pos(x, y - 1));
            return ret;
        }

        void MoveBlizzards()
        {
            foreach (var item in blizzards)
            {
                var cx = item.X;
                var cy = item.Y;

                switch (item.T)
                {
                    case '>':
                        if (item.X < xMax - 2)
                            item.X++;
                        else
                            item.X = 1;
                        break;

                    case '<':
                        if (item.X > 1)
                            item.X--;
                        else
                            item.X = xMax - 2;
                        break;

                    case '^':
                        if (item.Y > 1)
                            item.Y--;
                        else
                            item.Y = yMax - 2;
                        break;

                    case 'v':
                        if (item.Y < yMax - 2)
                            item.Y++;
                        else
                            item.Y = 1;
                        break;

                    default:
                        throw new Exception();
                }

                world[item.X, item.Y]++;
                world[cx, cy]--;
            }
        }
    }
}
