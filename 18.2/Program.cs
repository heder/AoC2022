class Cell
{
    public Cell(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    public int x;
    public int y;
    public int z;

    public bool visited = false;
    public bool isLava = false;

    public bool xl = false;
    public bool xr = false;
    public bool yl = false;
    public bool yr = false;
    public bool zl = false;
    public bool zr = false;
}


class Program
{
    static Cell[,,] world;

    static int xmin = int.MaxValue;
    static int xmax = int.MinValue;
    static int ymin = int.MaxValue;
    static int ymax = int.MinValue;
    static int zmin = int.MaxValue;
    static int zmax = int.MinValue;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        foreach (var item in lines)
        {
            var x = item.Split(',').Select(int.Parse).ToArray();

            xmin = Math.Min(xmin, x[0]);
            xmax = Math.Max(xmax, x[0]);

            ymin = Math.Min(ymin, x[1]);
            ymax = Math.Max(ymax, x[1]);

            zmin = Math.Min(zmin, x[2]);
            zmax = Math.Max(zmax, x[2]);
        }

        world = new Cell[xmax + 3, ymax + 3, zmax + 3];

        for (int x = 0; x < xmax + 3; x++)
        {
            for (int y = 0; y < ymax + 3; y++)
            {
                for (int z = 0; z < zmax + 3; z++)
                {
                    world[x, y, z] = new Cell(x, y, z);
                }
            }
        }

        foreach (var item in lines)
        {
            var x = item.Split(',').Select(int.Parse).ToArray();
            world[x[0] + 1, x[1] + 1, x[2] + 1].isLava = true;
        }

        SpreadAir(0,0,0);

        var c = world.Cast<Cell>().Where(f => f.visited == true).Count();

        var xl = world.Cast<Cell>().Where(f => f.xl == true).Count();
        var xr = world.Cast<Cell>().Where(f => f.xr == true).Count();
        var yl = world.Cast<Cell>().Where(f => f.yl == true).Count();
        var yr = world.Cast<Cell>().Where(f => f.yr == true).Count();
        var zl = world.Cast<Cell>().Where(f => f.zl == true).Count();
        var zr = world.Cast<Cell>().Where(f => f.zr == true).Count();

        Console.WriteLine(xl+xr+yl+yr+zr+zl);
        Console.ReadKey();
    }

    private static void SpreadAir(int x, int y, int z)
    {
        var r = GetReachable(x, y, z);
        foreach (var item in r)
        {
            SpreadAir(item.x, item.y, item.z);
        }
    }

    private static List<Cell> GetReachable(int x, int y, int z)
    {
        var l = new List<Cell>();

        if (x > 0)
        {
            var i = world[x - 1, y, z];
            if (i.isLava == true)
            {
                i.xr = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        if (x <= xmax+1)
        {
            var i = world[x + 1, y, z];
            if (i.isLava == true)
            {
                i.xl = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        if (y > 0)
        {
            var i = world[x, y - 1, z];
            if (i.isLava == true)
            {
                i.yr = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        if (y <= ymax+1)
        {
            var i = world[x, y + 1, z];
            if (i.isLava == true)
            {
                i.yl = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        if (z > 0)
        {
            var i = world[x, y, z - 1];
            if (i.isLava == true)
            {
                i.zr = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        if (z <= zmax+1)
        {
            var i = world[x, y, z + 1];
            if (i.isLava == true)
            {
                i.zl = true;
            }
            else if (i.visited == false)
            {
                i.visited = true;
                l.Add(i);
            }
        }

        return l;
    }
}
