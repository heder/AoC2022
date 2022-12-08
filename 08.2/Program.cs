class Program
{
    static int yLen;
    static int xLen;
    static int[,] forest;
    static int[,] score;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yLen = lines.Length;
        xLen = lines[0].Length;

        forest = new int[xLen, yLen];
        score = new int[xLen, yLen];

        for (int y = 0; y < yLen; y++)
        {
            for (int x = 0; x < xLen; x++)
            {
                forest[x, y] = Convert.ToInt32(lines[y][x].ToString());
            }
        }

        for (int y = 1; y < yLen - 1; y++)
        {
            for (int x = 1; x < xLen - 1; x++)
            {
                score[x, y] = IsVisible(x, y);
                Console.WriteLine(score[x, y]);
            }
        }

        var max = score.Cast<int>().Max();
        Console.WriteLine(max);
        Console.ReadKey();
    }


    private static int IsVisible(int xin, int yin)
    {
        int treeHeight = forest[xin, yin];

        var uVec = new List<int>();
        var dVec = new List<int>();
        var lVec = new List<int>();
        var rVec = new List<int>();

        // (Y-) (up)
        for (int y = yin - 1; y >= 0; y--)
        {
            uVec.Add(forest[xin, y]);
        }

        // Y+ (down)
        for (int y = yin + 1; y < yLen; y++)
        {
            dVec.Add(forest[xin, y]);
        }

        // X- (left)
        for (int x = xin - 1; x >= 0; x--)
        {
            lVec.Add(forest[x, yin]);
        }

        // X+ (right)
        for (int x = xin + 1; x < xLen; x++)
        {
            rVec.Add(forest[x, yin]);
        }

        var u = uVec.TakeWhile(f => f < treeHeight).Count();
        var l = lVec.TakeWhile(f => f < treeHeight).Count();
        var r = rVec.TakeWhile(f => f < treeHeight).Count();
        var d = dVec.TakeWhile(f => f < treeHeight).Count();

        if (u != uVec.Count) u++;
        if (l != lVec.Count) l++;
        if (r != rVec.Count) r++;
        if (d != dVec.Count) d++;

        return u * l * r * d;
    }
}
