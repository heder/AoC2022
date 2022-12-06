class Program
{
    static void Main()
    {
        var line = File.ReadAllText("in.txt").ToArray();

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i..].Take(14).Distinct().Count() == 14)
            {
                Console.WriteLine(i + 14);
                Console.ReadKey();
            }
        }
    }
}