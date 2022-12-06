class Program
{
    static void Main()
    {
        var line = File.ReadAllText("in.txt").ToArray();

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i..].Take(4).Distinct().Count() == 4)
            {
                Console.WriteLine(i + 4);
                Console.ReadKey();
                break;
            }
        }
    }
}