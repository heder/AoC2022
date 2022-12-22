internal class Cell
{
    public Cell(char type, int x, int y)
    {
        T = type;
        P = T;
        X = x;
        Y = y;
    }

    public int side;

    public int X;
    public int Y;

    public char T;
    public char P;
}



class Program
{
    static bool dump = false;

    class Pos
    {
        public int x = 0;
        public int y = 0;
    }

    static int yMax;
    static int xMax;
    static Cell[,] world;

    static Pos currentPos = new Pos();
    static int currentHeading = 0;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int i = 0;
        foreach (var item in lines)
        {
            if (item.All(f => f == ' '))
            {
                yMax = i;
                break;
            }

            i++;
        }

        string travelPath = lines[i + 1];

        xMax = lines.Max(f => f.Length);
        world = new Cell[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (x < lines[y].Length)
                    world[x, y] = new Cell(lines[y][x], x, y);
                else
                    world[x, y] = new Cell(' ', x, y);
            }
        }


        for (int y = 0; y < 50; y++)
        {
            for (int x = 50; x < 99; x++)
            {
                world[x, y].side = 1;
            }

            for (int x = 100; x < 150; x++)
            {
                world[x, y].side = 2;
            }
        }

        for (int y = 50; y < 100; y++)
        {
            for (int x = 50; x < 100; x++)
            {
                world[x, y].side = 3;
            }
        }

        for (int y = 100; y < 150; y++)
        {
            for (int x = 0; x < 50; x++)
            {
                world[x, y].side = 4;
            }

            for (int x = 50; x < 100; x++)
            {
                world[x, y].side = 5;
            }
        }

        for (int y = 150; y < 200; y++)
        {
            for (int x = 0; x < 50; x++)
            {
                world[x, y].side = 6;
            }
        }



        DumpWorld(0, xMax, 0, yMax);

        MoveToFirstValidPos();

        char rotation = 'R';
        int steps = 0;

        int cPos = 0;
        int rPos = 0;
        while (true)
        {
            while (true)
            {
                if (rPos == travelPath.Length || travelPath[rPos] == 'L' || travelPath[rPos] == 'R' || travelPath[rPos] == 'U' || travelPath[rPos] == 'D')
                {
                    steps = Convert.ToInt32(travelPath[cPos..rPos]);

                    Move(steps);

                    if (rPos == travelPath.Length)
                    {
                        int c = (1000 * (currentPos.y + 1)) + (4 * (currentPos.x + 1)) + currentHeading;
                        Console.WriteLine(c);
                        Console.ReadKey();
                    }

                    rotation = travelPath[rPos];


                    Rotate(rotation);

                    rPos++;
                    cPos = rPos;
                    break;
                }

                rPos++;

            }
        }


    }

    private static void Move(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            switch (currentHeading)
            {

                case 0:
                    currentPos.x = GetNextValidRight(currentPos.x, currentPos.y);
                    world[currentPos.x, currentPos.y].P = '>';
                    DumpWorld(0, xMax, 0, yMax);
                    break;

                case 1:
                    currentPos.y = GetNextValidDown(currentPos.x, currentPos.y);
                    world[currentPos.x, currentPos.y].P = 'v';
                    DumpWorld(0, xMax, 0, yMax);
                    break;

                case 2:
                    currentPos.x = GetNextValidLeft(currentPos.x, currentPos.y);
                    world[currentPos.x, currentPos.y].P = '<';
                    DumpWorld(0, xMax, 0, yMax);
                    break;

                case 3:
                    currentPos.y = GetNextValidUp(currentPos.x, currentPos.y);
                    world[currentPos.x, currentPos.y].P = '^';
                    DumpWorld(0, xMax, 0, yMax);
                    break;
            }
        }
    }



    static int GetNextValidUp(int x, int y)
    {
        if (y == 0 || world[x, y - 1].T == ' ')
        {
            int stay = y;

            while (true)
            {
                if (y == yMax - 1 || world[x, y + 1].T == ' ')
                {
                    break;
                }

                y++;
            }

            if (world[x, y].T == '.')
                return y;
            else
                return stay;
        }
        else
        {
            if (world[x, y - 1].T == '.')
                y--;
        }

        return y;
    }

    static int GetNextValidDown(int x, int y)
    {
        if (y == yMax - 1  || world[x, y + 1].T == ' ')
        {
            int stay = y;

            while (true)
            {
                if (y == 0 || world[x, y - 1].T == ' ')
                {
                    break;
                }

                y--;
            }

            if (world[x, y].T == '.')
                return y;
            else
                return stay;
        }
        else
        {
            if (world[x, y + 1].T == '.')
                y++;
        }

        return y;
    }

    static int GetNextValidLeft(int x, int y)
    {
        if (x == 0 || world[x - 1, y].T == ' ')
        {
            int stay = x;

            while (true)
            {
                if (x == xMax - 1 || world[x + 1, y].T == ' ')
                {
                    break;
                }

                x++;
            }

            if (world[x, y].T == '.')
                return x;
            else
                return stay;
        }
        else
        {
            if (world[x - 1, y].T == '.')
                x--;
        }

        return x;
    }

    static int GetNextValidRight(int x, int y)
    {
        if (x == xMax -1  || world[x + 1, y].T == ' ')
        {
            int stay = x;

            while (true)
            {
                if (x == 0 || world[x - 1, y].T == ' ')
                {
                    break;
                }

                x--;
            }

            if (world[x, y].T == '.')
                return x;
            else
                return stay;
        }
        else
        {
            if (world[x + 1, y].T == '.')
                x++;
        }

        return x;
    }



    private static void Rotate(char rotation)
    {
        if (rotation == 'R')
        {
            if (currentHeading == 3)
            {
                currentHeading = 0;
            }
            else
            {
                currentHeading++;
            }
        }

        if (rotation == 'L')
        {
            if (currentHeading == 0)
            {
                currentHeading = 3;
            }
            else
            {
                currentHeading--;
            }
        }
    }



    static void MoveToFirstValidPos()
    {
        while (world[currentPos.x, currentPos.y].T == ' ')
        {
            currentPos.x++;
        }
    }





    internal static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
    {
        if (dump == false) return;

        for (int y = ymin; y < ymax; y++)
        {
            for (int x = xmin; x < xmax; x++)
            {
                Console.Write(world[x, y].P);
            }

            Console.Write(Environment.NewLine);
        }

        Console.Write(Environment.NewLine);
    }



}
