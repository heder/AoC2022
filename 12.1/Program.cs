class Program
{
    static int yLen;
    static int xLen;
    static Cell[,] world;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yLen = lines.Length;
        xLen = lines[0].Length;

        world = new Cell[xLen, yLen];
        //int noVisible = (lines.Length * 2) + ((lines[0].Length - 2) * 2);

        for (int y = 0; y < yLen; y++)
        {
            for (int x = 0; x < xLen; x++)
            {
                world[x, y] = new Cell(lines[y][x]);  //Convert.ToInt32(lines[y][x].ToString());
            }
        }




        //for (int y = 1; y < yLen - 1; y++)
        //{
        //    for (int x = 1; x < xLen - 1; x++)
        //    {
        //        noVisible += IsVisible(x, y);
        //    }
        //}

        Console.WriteLine(noVisible);
        Console.ReadKey();
    }


    internal class Cell
    {
        public Cell(char height)
        {
            Height = height;
            Distance = 0;
        }

        public char Height;
        public int Distance;
    }



    private static int IsVisible(int xin, int yin)
    {
        int treeHeight = world[xin, yin];

        var uVec = new List<int>();
        var dVec = new List<int>();
        var lVec = new List<int>();
        var rVec = new List<int>();

        // (Y-) (up)
        for (int y = yin - 1; y >= 0; y--)
        {
            uVec.Add(world[xin, y]);
        }

        // Y+ (down)
        for (int y = yin + 1; y < yLen; y++)
        {
            dVec.Add(world[xin, y]);
        }

        // X- (left)
        for (int x = xin - 1; x >= 0; x--)
        {
            lVec.Add(world[x, yin]);
        }

        // X+ (right)
        for (int x = xin + 1; x < xLen; x++)
        {
            rVec.Add(world[x, yin]);
        }

        if (uVec.All(f => f < treeHeight) ||
            dVec.All(f => f < treeHeight) ||
            lVec.All(f => f < treeHeight) ||
            rVec.All(f => f < treeHeight))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
