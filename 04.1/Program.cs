class Program
{
    static void Main()
    {
        var lines = File.ReadLines("in.txt");
        int count = 0;

        foreach (var item in lines)
        {
            var s = item.Split('-', ',');
            var a = new MyRange(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
            var b = new MyRange(Convert.ToInt32(s[2]), Convert.ToInt32(s[3]));
            var c = new MyRange(Math.Min(a.Start, b.Start), Math.Max(a.End, b.End));

            if (c.Length() == a.Length() || c.Length() == b.Length())
            {
                count++;
            }
        }
          
        Console.WriteLine(count);
        Console.ReadKey();
    }

    public class MyRange
    {
        public MyRange(int s, int e)
        {
            Start = s; End = e;
        }

        public int Start { get; set; }
        public int End { get; set; }

        public int Length() 
        {
            return End - Start;
        }
    }
}
