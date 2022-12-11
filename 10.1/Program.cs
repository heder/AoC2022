class Program
{
    static int debugVal;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        var ops = new List<Op>();
        foreach (var item in lines)
        {
            var tokens = item.Split(' ');

            switch (tokens[0])
            {
                case "addx":
                    ops.Add(new Op() { Mnemonic = "addx", Operand = Convert.ToInt32(tokens[1]), Ticks = 2 });
                    break;

                case "noop":
                    ops.Add(new Op() { Mnemonic = "nop", Ticks = 1 });
                    break;

                default:
                    throw new Exception("");
            }
        }

        var cpu = new CPU();

        foreach (var item in ops)
        {
            cpu.Execute(item);
        }

        Console.WriteLine(debugVal);
        Console.ReadKey();
    }



    class CPU
    {
        public int Clock { get; set; }

        public int X { get; set; } = 1;

        public void Execute(Op o)
        {
            while (o.Ticks > 0)
            {
                Clock++;

                // If clock = debugger breakpoints
                if (Clock == 20 || (Clock - 20) % 40 == 0)
                {
                    var val = Clock * X;
                    debugVal += val;
                }
                //

                o.Ticks--;

                if (o.Ticks == 0) // Last microop, do work
                {
                    // Do the work
                    switch (o.Mnemonic)
                    {
                        case "addx":
                            X += o.Operand;
                            break;

                        case "nop":
                            break;

                        default:
                            throw new Exception("");
                    }
                }
            }
        }
    }



    class Op
    {
        public string Mnemonic { get; set; }
        public int Operand { get; set; }
        public int Ticks { get; set; }

    }

}
