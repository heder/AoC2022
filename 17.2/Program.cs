using System.Collections;
using System.Net.Security;

class Program
{
    abstract class Piece
    {
        public abstract void C(long r);

        public Pos[] Parts;

       // public long Height;
    }


    class P1 : Piece
    {
        public P1()
        {
            Parts = new Pos[4];
        }

        public override void C(long r)
        {
            Parts[0].X = 2;
            Parts[0].Y = 0;

            Parts[1].X = 3;
            Parts[1].Y = 0;

            Parts[2].X = 4;
            Parts[2].Y = 0;

            Parts[3].X = 5;
            Parts[3].Y = 0;

            for (int i = 0; i < 4; i++)
            {
                Parts[i].Y += r;
            }
        }
    }

    class P2 : Piece
    {

        public P2()
        {
            Parts = new Pos[5];
        }

        public override void C(long r)
        {
            Parts[0].X = 3;
            Parts[0].Y = 2;

            Parts[1].X = 2;
            Parts[1].Y = 1;

            Parts[2].X = 3;
            Parts[2].Y = 1;

            Parts[3].X = 4;
            Parts[3].Y = 1;

            Parts[4].X = 3;
            Parts[4].Y = 0;

            for (int i = 0; i < 5; i++)
            {
                Parts[i].Y += r;
            }
        }


        //public override void C(long r)
        //{
        //    //Height = 3;
        //    //r = Height;

        //    Parts.Add(new Pos(1, 2));
        //    Parts.Add(new Pos(0, 1));
        //    Parts.Add(new Pos(1, 1));
        //    Parts.Add(new Pos(2, 1));
        //    Parts.Add(new Pos(1, 0));

        //    foreach (var item in Parts)
        //    {
        //        item.Y += r;
        //        item.X += 2;
        //    }


        //}
    }

    class P3 : Piece
    {
        public P3()
        {
            Parts = new Pos[5];
        }

        public override void C(long r)
        {
            Parts[0].X = 4;
            Parts[0].Y = 2;

            Parts[1].X = 4;
            Parts[1].Y = 1;

            Parts[2].X = 4;
            Parts[2].Y = 0;

            Parts[3].X = 3;
            Parts[3].Y = 0;

            Parts[4].X = 2;
            Parts[4].Y = 0;

            for (int i = 0; i < 5; i++)
            {
                Parts[i].Y += r;
            }
        }

        //public override void C(long r)
        //{
        //    //Height = 3;
        //    //r += Height;

        //    Parts.Add(new Pos(2, 2));
        //    Parts.Add(new Pos(2, 1));
        //    Parts.Add(new Pos(2, 0));
        //    Parts.Add(new Pos(1, 0));
        //    Parts.Add(new Pos(0, 0));

        //    foreach (var item in Parts)
        //    {
        //        item.Y += r;
        //        item.X += 2;
        //    }


        //}
    }


    class P4 : Piece
    {

        public P4()
        {
            Parts = new Pos[4];
        }

        public override void C(long r)
        {
            Parts[0].X = 2;
            Parts[0].Y = 3;

            Parts[1].X = 2;
            Parts[1].Y = 2;

            Parts[2].X = 2;
            Parts[2].Y = 1;

            Parts[3].X = 2;
            Parts[3].Y = 0;



            for (int i = 0; i < 4; i++)
            {
                Parts[i].Y += r;
            }
        }

        //public new Pos[] Parts;

        //public override void C(long r)
        //{
        //    //Height += 4;
        //    //r = Height;

        //    Parts.Add(new Pos(0, 3));
        //    Parts.Add(new Pos(0, 2));
        //    Parts.Add(new Pos(0, 1));
        //    Parts.Add(new Pos(0, 0));

        //    foreach (var item in Parts)
        //    {
        //        item.Y += r;
        //        item.X += 2;
        //    }


        //}
    }

    class P5 : Piece
    {
        public P5()
        {
            Parts = new Pos[4];
        }

        public override void C(long r)
        {
            Parts[0].X = 2;
            Parts[0].Y = 1;

            Parts[1].X = 3;
            Parts[1].Y = 1;

            Parts[2].X = 2;
            Parts[2].Y = 0;

            Parts[3].X = 3;
            Parts[3].Y = 0;


            for (int i = 0; i < 4; i++)
            {
                Parts[i].Y += r;
            }
        }

        //public new Pos[] Parts;

        //public override void C(long r)
        //{
        //    //Height = 2;
        //    //r = Height;

        //    Parts.Add(new Pos(0, 1));
        //    Parts.Add(new Pos(1, 1));
        //    Parts.Add(new Pos(0, 0));
        //    Parts.Add(new Pos(1, 0));

        //    foreach (var item in Parts)
        //    {
        //        item.Y += r;
        //        item.X += 2;
        //    }
        //}
    }

    public struct Pos
    {
        public int X;
        public long Y;
    }


    //static char[,] world = new char[7, 100000];
    static Dictionary<long, char[]> world = new Dictionary<long, char[]>();


    static void Main()
    {
        long dictMax = 0;
        long upperlimit = 0;

        //Dictionary<long, string[]> dd = new Dictionary<long, string[]>();

        world.Add(0, "#######".ToCharArray());
        for (int i = 1; i < 20; i++)
        {
            world.Add(i, ".......".ToCharArray());
        }
        dictMax = 19;

        long floor = 0;

        char[] jets = File.ReadAllText("in.txt").ToCharArray();

        long pieceCount = 1;

        int currentPiece = 0;
        int currentJet = 0;
        long dropPosition;

        var p1 = new P1(); 
        var p2 = new P2(); 
        var p3 = new P3(); 
        var p4 = new P4(); 
        var p5 = new P5(); 


        while (true)
        {
            //DumpWorld(0, 7, 0, 20);

            dropPosition = upperlimit + 4;

            if (dropPosition + 10 > dictMax)
            {
                for (long i = dictMax + 1; i < dictMax + 10; i++)
                {
                    world.Add(i, ".......".ToCharArray());
                }

                dictMax += 9;
            }

            Piece p;
            switch (currentPiece)
            {
                case 0: p = p1; p.C(dropPosition); break;
                case 1: p = p2; p.C(dropPosition); break;
                case 2: p = p3; p.C(dropPosition); break;
                case 3: p = p4; p.C(dropPosition); break;
                case 4: p = p5; p.C(dropPosition); break;
                default:
                    throw new Exception("nope");
            }

            //DumpWorld(0, 7, 100000 - 20, 100000);

            // Fall
            while (true)
            {
                switch (jets[currentJet])
                {
                    case '<':
                        if (p.Parts.Any(f => f.X == 0) == false && p.Parts.All(f => world[f.Y][f.X - 1] == '.'))
                        {
                            for (int i = 0; i < p.Parts.Length; i++)
                            {
                                p.Parts[i].X--;
                            }

                            //foreach (var item in p.Parts)
                            //{
                            //    item.X--;
                            //}
                        }
                        break;

                    case '>':
                        if (p.Parts.Any(f => f.X == 6) == false && p.Parts.All(f => world[f.Y][f.X + 1] == '.'))
                        {
                            for (int i = 0; i < p.Parts.Length; i++)
                            {
                                p.Parts[i].X++;
                            }

                            //foreach (var item in p.Parts)
                            //{
                            //    item.X++;
                            //}
                        }
                        break;
                }

                if (currentJet >= jets.Length - 1)
                {
                    currentJet = 0;
                }
                else
                {
                    currentJet++;
                }

                // Down
                if (p.Parts.All(f => world[f.Y - 1][f.X] == '.'))
                {
                    for (int i = 0; i < p.Parts.Length; i++)
                    {
                        p.Parts[i].Y--;
                    }

                    //foreach (var item in p.Parts)
                    //{
                    //    item.Y--;
                    //}
                }
                else
                {
                    foreach (var item in p.Parts)
                    {
                        world[item.Y][item.X] = '@';
                    }
                    break;
                }
            }

            // If any of the rows affected by current piece is full
            // the topmost of these are the new floor, and everything below is removed
            foreach (var item in p.Parts)
            {
                if (world[item.Y].All(f => f == '@'))
                {
                    for (long i = floor; i <= item.Y - 1; i++)
                    {
                        world.Remove(i);
                    }
                    break;
                }
            }

            upperlimit = Math.Max(upperlimit, p.Parts.Max(p => p.Y));

            if (currentPiece > 3)
                currentPiece = 0;
            else
                currentPiece++;

            if (pieceCount == 1000000000000)
            {
                Console.WriteLine(upperlimit);
                Console.ReadKey();
            }

            if (pieceCount % 100000 == 0)
            {
                Console.WriteLine(pieceCount);
            }

            pieceCount++;
        }


        static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
        {
            for (int y = ymax - 1; y >= ymin; y--)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    Console.Write(world[y][x] == ' ' ? '.' : world[y][x]);
                }

                Console.Write(Environment.NewLine);
            }

            Console.Write(Environment.NewLine);
        }

    }
}




















//        foreach (var item in lines)
//        {
//            var coords = item.Split("->").Select(f => f.Trim()).ToArray();

//            for (int i = 0; i < coords.Length - 1; i++)
//            {
//                var from = coords[i].Split(",").Select(int.Parse).ToArray();
//                var to = coords[i + 1].Split(",").Select(int.Parse).ToArray();

//                for (int x = Math.Min(from[0], to[0]); x <= Math.Max(from[0], to[0]); x++)
//                {
//                    for (int y = Math.Min(from[1], to[1]); y <= Math.Max(from[1], to[1]); y++)
//                    {
//                        world[x, y] = '#';
//                    }
//                }
//            }
//        }

//        DumpWorld(490, 510, 0, 20);

//        int highestY = int.MinValue;

//        // Find lowest '#'
//        for (int x = 0; x < 1000; x++)
//        {
//            for (int y = 0; y < 1000; y++)
//            {
//                if (world[x, y] == '#')
//                {
//                    highestY = Math.Max(highestY, y);
//                    break;
//                }
//            }
//        }

//        int floorY = highestY + 2;

//        for (int x = 0; x < 1000; x++)
//        {
//            world[x, floorY] = '#';
//        }

//        int sandCount = 0;
//        Position sandPos = new Position();
//        bool atRest = true;
//        while (true)
//        {
//            if (atRest == true)
//            {
//                sandPos = new Position() { X = 500, Y = 0 };
//            }

//            // Tomt under
//            if (world[sandPos.X, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++; // d
//                atRest = false;
//            }
//            // Block under, ledigt snett vänster neråt.
//            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
//                //world[sandPos.X - 1, sandPos.Y] == '.' &&
//                world[sandPos.X - 1, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++;
//                sandPos.X--;
//                atRest = false;
//            }
//            // Block under, ledigt snett höger neråt.
//            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
//                //world[sandPos.X + 1, sandPos.Y] == '.' &&
//                world[sandPos.X + 1, sandPos.Y + 1] == '.')
//            {
//                sandPos.Y++;
//                sandPos.X++;
//                atRest = false;
//            }
//            else
//            {
//                world[sandPos.X, sandPos.Y] = 'o';
//                atRest = true;
//                sandCount++;
//            }

//            //DumpWorld(490, 510, 0, 20);

//            if (atRest == true && sandPos.X == 500 && sandPos.Y == 0)
//            {
//                Console.WriteLine(sandCount);
//                Console.ReadKey();
//            }
//        }
//    }





//}