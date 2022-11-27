using System;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadLines("in.txt").ToArray();

        int currentDepth = 0;
        int currentPosition = 0;


        for (int i = 0; i < lines.Length; i++)
        {
            var operation = lines[i].Split(' ');

            var op = operation[0];
            var val = Convert.ToInt32(operation[1]);

            switch(op)
            {
                case "forward":
                    currentPosition += val;
                    break;

                case "down":
                    currentDepth += val;
                    break;

                case "up":
                    currentDepth -= val;
                    break;
            }
        }

        Console.WriteLine(currentPosition * currentDepth);
        Console.ReadKey();
    }
}

