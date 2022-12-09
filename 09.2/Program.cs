using System.Xml.Serialization;

class Program
{
    static int yLen = 1000;
    static int xLen = 1000;
    static char[,] world;
    static bool[,] visitedByTail;
    //static Pos relative;
    static Pos[] ropePositions;


    static void Main()
    {
        //relative = new Pos(500, 500);

        ropePositions = new Pos[10];

        for (int i = 0; i < 10; i++)
        {
            ropePositions[i] = new Pos(500, 500);
        }

        
        visitedByTail = new bool[xLen, yLen];
        //visitedByTail[500, 500] = true;

        string[] lines = File.ReadAllLines("in.txt").ToArray();

        foreach (var line in lines)
        {
            var tokens = line.Split(' ');
            Move(tokens[0], Convert.ToInt32(tokens[1]));

            //DumpState();

            
        }

        yLen = lines.Length;
        xLen = lines[0].Length;

        var c = visitedByTail.Cast<bool>().Count(f => f == true);

        Console.WriteLine(c);
        Console.ReadKey();
    }


    static void Move(string direction, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            MovePos(ropePositions[0], direction);
            //DumpWorld(500, 510, 500, 510);

            for (int r = 1; r < ropePositions.Length; r++)
            {
                MoveTail(ropePositions[r], ropePositions[r - 1]);
                //DumpWorld(500, 510, 500, 510);
            }

            visitedByTail[ropePositions[9].X, ropePositions[9].Y] = true;
        }
    }

    static void MoveTail(Pos pos, Pos relative)
    {
        // Find "angular" distance to head
        int d = Math.Abs(relative.X - pos.X) + Math.Abs(relative.Y - pos.Y);

        if (relative.X != pos.X && relative.Y != pos.Y && d >= 3)
        {
            // Find direction to move tail
            if (relative.X > pos.X)
            {
                MovePos(pos, "R");
            }

            if (relative.X < pos.X)
            {
                MovePos(pos, "L");
            }

            if (relative.Y > pos.Y)
            {
                MovePos(pos, "U");
            }

            if (relative.Y < pos.Y)
            {
                MovePos(pos, "D");
            }
        }
        else if ((relative.X == pos.X || relative.Y == pos.Y) && d == 2)
        {
            // Find direction to move tail
            if (relative.X > pos.X)
            {
                MovePos(pos, "R");
            }

            if (relative.X < pos.X)
            {
                MovePos(pos, "L");
            }

            if (relative.Y > pos.Y)
            {
                MovePos(pos, "U");
            }

            if (relative.Y < pos.Y)
            {
                MovePos(pos, "D");
            }
        }

        //visitedByTail[pos.X, pos.Y] = true;
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
        world = new char[xLen, yLen];

        for (int y = ymax; y >= ymin; y--)
        {
            for (int x = xmin; x <= xmax; x++)
            {
                world[x,y] = '.';
            }

            Console.Write(Environment.NewLine);
        }


        for (int i = 9; i >= 0; i--)
        {
            world[ropePositions[i].X, ropePositions[i].Y] = Convert.ToChar(i.ToString());
        }

        for (int y = ymax; y >= ymin; y--)
        {
            for (int x = xmin; x <= xmax; x++)
            {
                Console.Write(world[x, y]);
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }


    //private static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    //{
    //    for (int y = ymax; y >= ymin; y--)
    //    {
    //        for (int x = xmin; x <= xmax; x++)
    //        {
    //            Console.Write(visitedByTail[x, y] ? "#" : ".");
    //        }

    //        Console.Write(Environment.NewLine);
    //    }

    //    Console.Write(Environment.NewLine);
    //}


    class Pos
    {
        public Pos(int x, int y)
        {
            X = x; Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }





    //private static int IsVisible(int xin, int yin)
    //{
    //    int treeHeight = forest[xin, yin];

    //    var uVec = new List<int>();
    //    var dVec = new List<int>();
    //    var lVec = new List<int>();
    //    var rVec = new List<int>();

    //    // (Y-) (up)
    //    for (int y = yin - 1; y >= 0; y--)
    //    {
    //        uVec.Add(forest[xin, y]);
    //    }

    //    // Y+ (down)
    //    for (int y = yin + 1; y < yLen; y++)
    //    {
    //        dVec.Add(forest[xin, y]);
    //    }

    //    // X- (left)
    //    for (int x = xin - 1; x >= 0; x--)
    //    {
    //        lVec.Add(forest[x, yin]);
    //    }

    //    // X+ (right)
    //    for (int x = xin + 1; x < xLen; x++)
    //    {
    //        rVec.Add(forest[x, yin]);
    //    }

    //    if (uVec.All(f => f < treeHeight) ||
    //        dVec.All(f => f < treeHeight) ||
    //        lVec.All(f => f < treeHeight) ||
    //        rVec.All(f => f < treeHeight))
    //    {
    //        return 1;
    //    }
    //    else
    //    {
    //        return 0;
    //    }
    //}
}
