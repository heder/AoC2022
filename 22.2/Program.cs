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

        xMax = lines.SkipLast(1).Max(f => f.Length);
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

        for (int y = 0; y <= 49; y++)
        {
            for (int x = 50; x <= 99; x++)
            {
                world[x, y].side = 1;
            }

            for (int x = 100; x <= 149; x++)
            {
                world[x, y].side = 2;
            }
        }

        for (int y = 50; y <= 99; y++)
        {
            for (int x = 50; x <= 99; x++)
            {
                world[x, y].side = 3;
            }
        }

        for (int y = 100; y <= 149; y++)
        {
            for (int x = 0; x <= 49; x++)
            {
                world[x, y].side = 4;
            }

            for (int x = 50; x <= 99; x++)
            {
                world[x, y].side = 5;
            }
        }

        for (int y = 150; y <= 199; y++)
        {
            for (int x = 0; x <= 49; x++)
            {
                world[x, y].side = 6;
            }
        }

        int check1 = world.Cast<Cell>().Count(f => f.side == 1);
        int check2 = world.Cast<Cell>().Count(f => f.side == 2);
        int check3 = world.Cast<Cell>().Count(f => f.side == 3);
        int check4 = world.Cast<Cell>().Count(f => f.side == 4);
        int check5 = world.Cast<Cell>().Count(f => f.side == 5);
        int check6 = world.Cast<Cell>().Count(f => f.side == 6);



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

                    DumpWorld(0, xMax, 0, yMax);

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
                    SetNextValidRight();
                    world[currentPos.x, currentPos.y].P = '>';
                    break;

                case 1:
                    SetNextValidDown();
                    world[currentPos.x, currentPos.y].P = 'v';
                    break;

                case 2:
                    SetNextValidLeft();
                    world[currentPos.x, currentPos.y].P = '<';
                    break;

                case 3:
                    SetNextValidUp();
                    world[currentPos.x, currentPos.y].P = '^';
                    break;
            }
        }
    }


    static void SetNextValidUp()
    {
        var x = currentPos.x;
        var y = currentPos.y;

        if (y == 100 && world[x, y].side == 4)
        {
            var px = 50;
            var py = 99 - (49 - x);

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 0;

                if (world[px, py].side != 3) throw new Exception();
            }
        }
        else if (y == 0 && world[x, y].side == 1)
        {
            var px = 0;
            var py = 199 - (99 - x);

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 0;

                if (world[px, py].side != 6) throw new Exception();
            }
        }
        else if (y == 0 && world[x, y].side == 2)
        {
            var px = 49 - (149 - x);
            var py = 199;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 3;

                if (world[px, py].side != 6) throw new Exception();
            }
        }
        else
        {
            if (world[x, y - 1].T == '.')
                currentPos.y--;
        }
    }

    static void SetNextValidDown()
    {
        var x = currentPos.x;
        var y = currentPos.y;

        if (y == 199 && world[x, y].side == 6)
        {
            var px = 149 - (49 - x);
            var py = 0;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 1;

                if (world[px, py].side != 2) throw new Exception();
            }
        }
        else if (y == 149 && world[x, y].side == 5)
        {
            var px = 49;
            var py = 199 - (99 - x);

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 2;

                if (world[px, py].side != 6) throw new Exception();
            }
        }
        else if (y == 49 && world[x, y].side == 2)
        {
            var px = 99;
            var py = 99 - (149 - x);

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 2;

                if (world[px, py].side != 3) throw new Exception();
            }
        }
        else
        {
            if (world[x, y + 1].T == '.')
                currentPos.y++;
        }
    }

    static void SetNextValidLeft()
    {
        var x = currentPos.x;
        var y = currentPos.y;

        if (x == 50 && world[x, y].side == 1)
        {
            var px = 0;
            var py = 149 - y;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 0;

                if (world[px, py].side != 4) throw new Exception();
            }
        }
        else if (x == 50 && world[x, y].side == 3)
        {
            var px = 49 - (99 - y);
            var py = 100;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 1;

                if (world[px, py].side != 4) throw new Exception();
            }
        }
        else if (x == 0 && world[x, y].side == 4)
        {
            var px = 50;
            var py = 0 + (149 - y);

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 0;

                if (world[px, py].side != 1) throw new Exception();
            }
        }
        else if (x == 0 && world[x, y].side == 6)
        {
            var px = 99 - (199 - y);
            var py = 0;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 1;

                if (world[px, py].side != 1) throw new Exception();
            }
        }
        else
        {
            if (world[x - 1, y].T == '.')
                currentPos.x--;
        }

    }

    static void SetNextValidRight()
    {
        var x = currentPos.x;
        var y = currentPos.y;

        if (x == 149 && world[x, y].side == 2)
        {
            var px = 99;
            var py = 149 - y;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 2;

                if (world[px, py].side != 5) throw new Exception();
            }
        }
        else if (x == 99 && world[x, y].side == 3)
        {
            var px = 149 - (99 - y);
            var py = 49;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 3;

                if (world[px, py].side != 2) throw new Exception();
            }
        }
        else if (x == 99 && world[x, y].side == 5)
        {
            var px = 149;
            var py = 149 - y;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 2;

                if (world[px, py].side != 2) throw new Exception();
            }
        }
        else if (x == 49 && world[x, y].side == 6)
        {
            var px = 99 - (199 - y);
            var py = 149;

            if (world[px, py].T == '.')
            {
                currentPos.x = px;
                currentPos.y = py;
                currentHeading = 3;

                if (world[px, py].side != 5) throw new Exception();
            }
        }
        else
        {
            if (world[x + 1, y].T == '.')
                currentPos.x++;
        }
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
        Console.ReadKey();
    }
}
