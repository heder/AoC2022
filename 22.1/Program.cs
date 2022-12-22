
internal class Cell
{
    public Cell(char type, int x, int y)
    {
        Type = type;
        //OriginalHeight = height;
        //Distance = int.MaxValue;
        X = x;
        Y = y;
    }

    public int X;
    public int Y;

    public char Type;
    //public char OriginalHeight;

    //public int Distance;
}



class Program
{
    class Pos
    {
        public int x = 0;
        public int y = 0;
    }

    static int yMax;
    static int xMax;
    static Cell[,] world;

    static Pos currentPos = new Pos();
    static char currentHeading = 'R';

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int i = 0;
        foreach (var item in lines)
        {
            if (item[0] == ' ')
            {
                yMax = i; break;
            }

            i++;
        }

        string travelPath = lines[i + 1];

        xMax = lines[0].Length;
        world = new Cell[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y] = new Cell(lines[y][x], x, y);
            }
        }


        MoveToFirstValidPos();

        char rotation;
        int steps;

        int cPos = 0;
        while (true)
        {
            int x = cPos + 1;

            if (travelPath[x] == 'L' || travelPath[x] == 'R' || travelPath[x] == 'U' || travelPath[x] == 'D'))
            {
                steps = Convert.ToInt32(travelPath[cPos..x]);
                rotation = travelPath[x];
            }


            Move(steps);
            Rotate(rotation);
        }

        //Console.WriteLine(lengths.Min());
        Console.ReadKey();
    }

    private static void Move(int steps)
    {
        switch (currentHeading)
        {
            case 'U':
                if ()



                    break;

            case 'D':
                break;

            case 'L':
                break;

            case 'R':
                break;

        }
    }



    int GetNextValidUp(int x, int y)
    {
        if (y == 0 || world[x, y - 1].Type == ' ')
        {
            // Find first valid non-' ' at bottom
            while (true)
            {
                if (world[x, y + 1].Type == ' ')
                { 
                    break; 
                }

                y++;
            }
        }
        else
        {
            y--;
        }

        return y;
    }

    int GetNextValidDown(int x, int y)
    {
        if (y == 0 || world[x, y + 1].Type == ' ')
        {
            // Find first valid non-' ' at bottom
            while (true)
            {
                if (world[x, y - 1].Type == ' ')
                {
                    break;
                }

                y--;
            }
        }
        else
        {
            y++;
        }

        return y;
    }

    int GetNextValidLeft(int x, int y)
    {
        if (y == 0 || world[x - 1, y].Type == ' ')
        {
            // Find first valid non-' ' at bottom
            while (true)
            {
                if (world[x - 1, y].Type == ' ')
                {
                    break;
                }

                x++;
            }
        }
        else
        {
            x--;
        }

        return x;
    }

    int GetNextValidRight(int x, int y)
    {
        if (y == 0 || world[x + 1, y].Type == ' ')
        {
            // Find first valid non-' ' at bottom
            while (true)
            {
                if (world[x + 1, y].Type == ' ')
                {
                    break;
                }

                x--;
            }
        }
        else
        {
            x++;
        }

        return x;
    }



    private static void Rotate(char rotation)
    {
        currentHeading = rotation;
    }



    static void MoveToFirstValidPos()
    {
        while (world[currentPos.x, currentPos.y].Type == ' ')
        {
            currentPos.x++;
        }
    }


}

//    private static void TraverseWorld(Cell c)
//    {
//        //DumpWorld(0, xMax, 0, yMax);

//        if (c.Height == 'E')
//        {
//            return;
//        }

//        var paths = GetPaths(c);
//        foreach (var item in paths)
//        {
//            item.Distance = c.Distance + 1;
//            TraverseWorld(item);
//        }

//        return;
//    }

//    internal static List<Cell> GetPaths(Cell c)
//    {
//        var positions = new List<Cell>();

//        if (c.Y > 0)
//        {
//            var u = world[c.X, c.Y - 1];

//            if (CheckDistanceAndHeight(c, u))
//            {
//                positions.Add(u);
//            }
//        }

//        if (c.Y < yMax - 1)
//        {
//            var d = world[c.X, c.Y + 1];

//            if (CheckDistanceAndHeight(c, d))
//            {
//                positions.Add(d);
//            }
//        }

//        if (c.X > 0)
//        {
//            var l = world[c.X - 1, c.Y];

//            if (CheckDistanceAndHeight(c, l))
//            {
//                positions.Add(l);
//            }
//        }

//        if (c.X < xMax - 1)
//        {
//            var r = world[c.X + 1, c.Y];

//            if (CheckDistanceAndHeight(c, r))
//            {
//                positions.Add(r);
//            }
//        }

//        return positions;
//    }

//    //private static bool CheckDistanceAndHeight(Cell c, Cell d)
//    //{
//    //    return c.Distance + 1 < d.Distance && ((d.Height <= c.Height + 1 && d.Height != 'E') || (c.Height == 'z' && d.Height == 'E') || (c.Height == 'S' && d.Height == 'a'));
//    //}


//    //private static void ResetWorld()
//    //{
//    //    foreach (var item in world)
//    //    {
//    //        item.Height = item.OriginalHeight;
//    //        item.Distance = int.MaxValue;
//    //    }
//    //}




//    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
//    {
//        for (int y = ymin; y < ymax; y++)
//        {
//            for (int x = xmin; x < xmax; x++)
//            {
//                Console.Write(world[x, y]);
//            }

//            Console.Write(Environment.NewLine);
//        }

//        Console.Write(Environment.NewLine);
//    }
//}
