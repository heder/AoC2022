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
        var resultToPrint = s;
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






//class Program

//{
//    static void Main()
//    {


//        string[] lines = File.ReadAllLines("in.txt");


//        int upperacc = 0;
//        foreach (var item in lines)
//        {
//            int from = FromSnafu(item);

//            upperacc += from;

//            Console.WriteLine(from);
//        }

//        Console.WriteLine(upperacc);



//        for (int i = 0; i < int.MaxValue; i++)
//        {
//            var x = DecimalToArbitrarySystem(i, 5);
//            string snafu = "";
//            for (int j = 0; j < x.Length; j++)
//            {
//                snafu += GetX(x[j]);
//            }

//            int e = FromSnafu(snafu);
//            if (e == upperacc)
//            {
//                Console.WriteLine(snafu);
//                Console.ReadKey();
//            }


//            if (i % 1 == 0) Console.WriteLine(i + " " + e);

//        }



//        int FromSnafu(string item)
//        {
//            var num = item.Reverse().ToArray();
//            int acc = 0;
//            for (int i = 0; i < num.Length; i++)
//            {
//                int c = (int)Math.Pow(5, i);
//                int n = GetVal(num[i]);
//                acc += n * c;
//            }

//            return acc;
//        }



//        char GetX(char v)
//        {
//            switch (v)
//            {
//                case '2': return '0';
//                case '3': return '1';
//                case '4': return '2';
//                case '0': return '=';
//                case '1': return '-';

//                default:
//                    throw new Exception();
//            }
//        }


//        string DecimalToArbitrarySystem(long decimalNumber, int radix)
//        {
//            const int BitsInLong = 64;
//            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

//            if (radix < 2 || radix > Digits.Length)
//                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

//            if (decimalNumber == 0)
//                return "0";

//            int index = BitsInLong - 1;
//            long currentNumber = Math.Abs(decimalNumber);
//            char[] charArray = new char[BitsInLong];

//            while (currentNumber != 0)
//            {
//                int remainder = (int)(currentNumber % radix);
//                charArray[index--] = Digits[remainder];
//                currentNumber = currentNumber / radix;
//            }

//            string result = new String(charArray, index + 1, BitsInLong - index - 1);
//            if (decimalNumber < 0)
//            {
//                result = "-" + result;
//            }

//            return result;
//        }
//    }
//}