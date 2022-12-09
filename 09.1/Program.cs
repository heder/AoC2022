class Program
{
    static int yLen = 1000;
    static int xLen = 1000;
    static bool[,] visitedByTail;
    static Pos headPos;
    static Pos tailPos;

    static void Main()
    {
        headPos = new Pos(500, 500);
        tailPos = new Pos(500, 500);
        
        visitedByTail = new bool[xLen, yLen];

        string[] lines = File.ReadAllLines("in.txt").ToArray();

        foreach (var line in lines)
        {
            var tokens = line.Split(' ');
            MoveHead(tokens[0], Convert.ToInt32(tokens[1]));

            //DumpWorld(500, 510, 500, 510);
        }

        yLen = lines.Length;
        xLen = lines[0].Length;

        var c = visitedByTail.Cast<bool>().Count(f => f == true);

        Console.WriteLine(c);
        Console.ReadKey();
    }


    static void MoveHead(string direction, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            MovePos(headPos, direction);
            MoveTail(tailPos);
        }
    }

    static void MoveTail(Pos pos)
    {
        // Find "angular" distance to head
        int d = Math.Abs(headPos.X - tailPos.X) + Math.Abs(headPos.Y - tailPos.Y);

        if (headPos.X != tailPos.X && headPos.Y != tailPos.Y && d == 3)
        {
            // Find direction to move tail
            if (headPos.X > tailPos.X)
            {
                MovePos(tailPos, "R");
            }

            if (headPos.X < tailPos.X)
            {
                MovePos(tailPos, "L");
            }

            if (headPos.Y > tailPos.Y)
            {
                MovePos(tailPos, "U");
            }

            if (headPos.Y < tailPos.Y)
            {
                MovePos(tailPos, "D");
            }
        }
        else if ((headPos.X == tailPos.X || headPos.Y == tailPos.Y) && d == 2)
        {
            // Find direction to move tail
            if (headPos.X > tailPos.X)
            {
                MovePos(tailPos, "R");
            }

            if (headPos.X < tailPos.X)
            {
                MovePos(tailPos, "L");
            }

            if (headPos.Y > tailPos.Y)
            {
                MovePos(tailPos, "U");
            }

            if (headPos.Y < tailPos.Y)
            {
                MovePos(tailPos, "D");
            }
        }

        visitedByTail[tailPos.X, tailPos.Y] = true;
    }

    private static void MovePos(Pos p, string direction)
    {
        switch (direction)
        {
            case "U": p.Y++; break;
            case "L": p.X--; break;
            case "R": p.X++; break;
            case "D": p.Y--; break;

            default: break;
        }
    }

    private static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    {
        for (int y = ymax; y >= ymin; y--)
        {
            for (int x = xmin; x <= xmax; x++)
            {
                Console.Write(visitedByTail[x,y] ? "#" : ".");
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }

    class Pos
    {
        public Pos(int x, int y)
        {
            X = x; Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
