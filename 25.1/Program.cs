class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("in.txt").ToArray();
        var numbers = new List<long>();

        for (int i = 0; i < lines.Length; i++)
        {

            var num = lines[i].Reverse().ToArray();
            long acc = 0;
            for (int j = 0; j < num.Length; j++)
            {
                long c = (long)Math.Pow(5, j);
                long n = GetVal(num[j]);
                acc += n * c;
            }

            numbers.Add(acc);
        }

        var s = numbers.Sum();
        var snafu = "";

        while (s > 0)
        {
            var number = s % 5;
            s /= 5;

            switch (number)
            {
                case 0:
                    snafu = "0" + snafu;
                    break;
                case 1:
                    snafu = "1" + snafu;
                    break;
                case 2:
                    snafu = "2" + snafu;
                    break;
                case 3:
                    snafu = "=" + snafu;
                    s++;
                    break;
                case 4:
                    snafu = "-" + snafu;
                    s++;
                    break;
                default:
                    throw new Exception();
            }
        }

        Console.WriteLine(snafu);
        Console.ReadKey();


        long GetVal(char v)
        {
            switch (v)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '-': return -1;
                case '=': return -2;

                default:
                    return 42;
            }
        }
    }
}





