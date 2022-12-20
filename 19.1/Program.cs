using System.Text.RegularExpressions;

class Program
{

    static int ore = 0;
    static int clay = 0;
    static int obsidian = 0;
    static int geode = 0;

    static int oreCollectors = 1;
    static int clayCollectors = 0;
    static int obsidianCollectors = 0;
    static int geodeCollectors = 0;

    static int oreCollectorOreCost;
    static int clayCollectorOreCost;

    static int obsidianCollectorOreCost;
    static int obsidianCollectorClayCost;

    static int geodeCollectorOreCost;
    static int geodeCollectorObsidianCost;


    static void Main()
    {
        var lines = File.ReadAllLines("in.txt");

        foreach (var item in lines)
        {
            var l0 = item.Split(":");
            var l1 = l0[1].Split(".");
            var l2 = l1[0].Split(" ");
            oreCollectorOreCost = int.Parse(l2[5]);

            var l3 = l1[1].Split(" ");
            clayCollectorOreCost = int.Parse(l3[5]);

            var l4 = l1[2].Split(" ");
            obsidianCollectorOreCost = int.Parse(l4[5]);
            obsidianCollectorClayCost = int.Parse(l4[8]);

            var l5 = l1[3].Split(" ");
            geodeCollectorOreCost = int.Parse(l5[5]);
            geodeCollectorObsidianCost = int.Parse(l5[8]);

            List<int[]> opOrders = new List<int[]>();

            while (true)
            {
                ore = 0;
                clay = 0;
                obsidian = 0;
                geode = 0;

                oreCollectors = 1;
                clayCollectors = 0;
                obsidianCollectors = 0;
                geodeCollectors = 0;

                int[] opArray = new int[25];

                for (int minute = 1; minute <= 24; minute++)
                {
                    int AoreCollectors = oreCollectors;
                    int AclayCollectors = clayCollectors;
                    int AobsidianCollectors = obsidianCollectors;
                    int AgeodeCollectors = geodeCollectors;


                    bool goAgain = true;
                    while (goAgain)
                    {
                        goAgain = false;

                        if (opOrders.Any(f => f[minute] == 0) == false)
                        {
                            opArray[minute] = 0;
                        }

                        if (opOrders.Any(f => f[minute] == 1) == false && ore >= geodeCollectorOreCost && obsidian >= geodeCollectorObsidianCost)
                        {
                            geodeCollectors++;
                            ore -= geodeCollectorOreCost;
                            obsidian -= geodeCollectorObsidianCost;
                            goAgain = true;
                            opArray[minute] = 1;
                        }

                        if (opOrders.Any(f => f[minute] == 2) == false && ore >= obsidianCollectorOreCost && clay >= obsidianCollectorClayCost)
                        {
                            obsidianCollectors++;
                            ore -= obsidianCollectorOreCost;
                            clay -= obsidianCollectorClayCost;
                            goAgain = true;
                            opArray[minute] = 2;
                        }

                        if (opOrders.Any(f => f[minute] == 3) == false && ore >= clayCollectorOreCost)
                        {
                            clayCollectors++;
                            ore -= clayCollectorOreCost;
                            goAgain = true;
                            opArray[minute] = 3;
                        }

                        if (opOrders.Any(f => f[minute] == 4) == false && ore >= oreCollectorOreCost)
                        {
                            oreCollectors++;
                            ore -= oreCollectorOreCost;
                            goAgain = true;
                            opArray[minute] = 4;
                        }
                    }
                


                // Collect
                ore += AoreCollectors;
                clay += AclayCollectors;
                obsidian += AobsidianCollectors;
                geode += AgeodeCollectors;

                Console.WriteLine($"{minute} ore:{ore} ({oreCollectors}) clay:{clay} ({clayCollectors}) obsidian:{obsidian} ({obsidianCollectors}) geode:{geode} ({geodeCollectors})");


            }

            opOrders.Add(opArray);

            Console.WriteLine($"GEODES: {geode}");
        }

    }


}
}


//        int upperlimit = 99999;

//        for (int x = 0; x < 7; x++)
//        {
//            for (int y = 0; y < 100000; y++)
//            {
//                world[x, y] = '.';
//            }
//        }

//        for (int i = 0; i < 7; i++)
//        {
//            world[i, 99999] = '#';
//        }



//        int pieceCount = 1;

//        int currentPiece = 0;
//        int currentJet = 0;
//        while (true)
//        {
//            //DumpWorld(0, 7, 100000 - 20, 100000);

//            int dropPosition = upperlimit - 3;

//            Piece p;
//            switch (currentPiece)
//            {
//                case 0: p = new P1(dropPosition); break;
//                case 1: p = new P2(dropPosition); break;
//                case 2: p = new P3(dropPosition); break;
//                case 3: p = new P4(dropPosition); break;
//                case 4: p = new P5(dropPosition); break;
//                default:
//                    throw new Exception("nope");
//            }

//            //DumpWorld(0, 7, 100000 - 20, 100000);

//            // Fall
//            while (true)
//            {
//                switch (jets[currentJet])
//                {
//                    case '<':
//                        if (p.Parts.All(f => f.X > 0) && p.Parts.All(f => world[f.X - 1, f.Y] == '.'))
//                        {
//                            foreach (var item in p.Parts)
//                            {
//                                item.X--;
//                            }
//                        }
//                        break;

//                    case '>':
//                        if (p.Parts.All(f => f.X < 6) && p.Parts.All(f => world[f.X + 1, f.Y] == '.'))
//                        {
//                            foreach (var item in p.Parts)
//                            {
//                                item.X++;
//                            }
//                        }
//                        break;
//                }

//                if (currentJet >= jets.Length - 1)
//                {
//                    currentJet = 0;
//                }
//                else
//                {
//                    currentJet++;
//                }

//                // Down
//                if (p.Parts.All(f => world[f.X, f.Y + 1] == '.'))
//                {
//                    foreach (var item in p.Parts)
//                    {
//                        item.Y++;
//                    }
//                }
//                else
//                {
//                    foreach (var item in p.Parts)
//                    {
//                        world[item.X, item.Y] = '@';
//                    }
//                    break;
//                }
//            }


//            upperlimit = Math.Min(upperlimit, p.Parts.Min(p => p.Y));

//            if (currentPiece > 3)
//                currentPiece = 0;
//            else
//                currentPiece++;

//            if (pieceCount == 2022)
//            {
//                Console.WriteLine(99999 - upperlimit);
//                Console.ReadKey();
//            }

//            pieceCount++;
//        }


//        static void DumpWorld(int xmin, int xmax, int ymin, int ymax)
//        {
//            for (int y = ymin; y < ymax; y++)
//            {
//                for (int x = xmin; x < xmax; x++)
//                {
//                    Console.Write(world[x, y] == ' ' ? '.' : world[x, y]);
//                }

//                Console.Write(Environment.NewLine);
//            }

//            Console.Write(Environment.NewLine);
//        }

//    }
//}




















////        foreach (var item in lines)
////        {
////            var coords = item.Split("->").Select(f => f.Trim()).ToArray();

////            for (int i = 0; i < coords.Length - 1; i++)
////            {
////                var from = coords[i].Split(",").Select(int.Parse).ToArray();
////                var to = coords[i + 1].Split(",").Select(int.Parse).ToArray();

////                for (int x = Math.Min(from[0], to[0]); x <= Math.Max(from[0], to[0]); x++)
////                {
////                    for (int y = Math.Min(from[1], to[1]); y <= Math.Max(from[1], to[1]); y++)
////                    {
////                        world[x, y] = '#';
////                    }
////                }
////            }
////        }

////        DumpWorld(490, 510, 0, 20);

////        int highestY = int.MinValue;

////        // Find lowest '#'
////        for (int x = 0; x < 1000; x++)
////        {
////            for (int y = 0; y < 1000; y++)
////            {
////                if (world[x, y] == '#')
////                {
////                    highestY = Math.Max(highestY, y);
////                    break;
////                }
////            }
////        }

////        int floorY = highestY + 2;

////        for (int x = 0; x < 1000; x++)
////        {
////            world[x, floorY] = '#';
////        }

////        int sandCount = 0;
////        Position sandPos = new Position();
////        bool atRest = true;
////        while (true)
////        {
////            if (atRest == true)
////            {
////                sandPos = new Position() { X = 500, Y = 0 };
////            }

////            // Tomt under
////            if (world[sandPos.X, sandPos.Y + 1] == '.')
////            {
////                sandPos.Y++; // d
////                atRest = false;
////            }
////            // Block under, ledigt snett vänster neråt.
////            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
////                //world[sandPos.X - 1, sandPos.Y] == '.' &&
////                world[sandPos.X - 1, sandPos.Y + 1] == '.')
////            {
////                sandPos.Y++;
////                sandPos.X--;
////                atRest = false;
////            }
////            // Block under, ledigt snett höger neråt.
////            else if (world[sandPos.X, sandPos.Y + 1] != '.' &&
////                //world[sandPos.X + 1, sandPos.Y] == '.' &&
////                world[sandPos.X + 1, sandPos.Y + 1] == '.')
////            {
////                sandPos.Y++;
////                sandPos.X++;
////                atRest = false;
////            }
////            else
////            {
////                world[sandPos.X, sandPos.Y] = 'o';
////                atRest = true;
////                sandCount++;
////            }

////            //DumpWorld(490, 510, 0, 20);

////            if (atRest == true && sandPos.X == 500 && sandPos.Y == 0)
////            {
////                Console.WriteLine(sandCount);
////                Console.ReadKey();
////            }
////        }
////    }





////}
///





//class Piece
//{
//    public List<Pos> Parts = new List<Pos>();
//    public int Height;
//}


//class P1 : Piece
//{
//    public P1(int r)
//    {
//        Height = 1;
//        r -= Height;

//        Parts.Add(new Pos(0, 0));
//        Parts.Add(new Pos(1, 0));
//        Parts.Add(new Pos(2, 0));
//        Parts.Add(new Pos(3, 0));

//        foreach (var item in Parts)
//        {
//            item.Y += r;
//            item.X += 2;
//        }
//    }
//}

//class P2 : Piece
//{
//    public P2(int r)
//    {
//        Height = 3;
//        r -= Height;

//        Parts.Add(new Pos(1, 0));
//        Parts.Add(new Pos(0, 1));
//        Parts.Add(new Pos(1, 1));
//        Parts.Add(new Pos(2, 1));
//        Parts.Add(new Pos(1, 2));

//        foreach (var item in Parts)
//        {
//            item.Y += r;
//            item.X += 2;
//        }


//    }
//}

//class P3 : Piece
//{
//    public P3(int r)
//    {
//        Height = 3;
//        r -= Height;

//        Parts.Add(new Pos(2, 0));
//        Parts.Add(new Pos(2, 1));
//        Parts.Add(new Pos(2, 2));
//        Parts.Add(new Pos(0, 2));
//        Parts.Add(new Pos(1, 2));

//        foreach (var item in Parts)
//        {
//            item.Y += r;
//            item.X += 2;
//        }


//    }
//}


//class P4 : Piece
//{
//    public P4(int r)
//    {
//        Height = 4;
//        r -= Height;

//        Parts.Add(new Pos(0, 0));
//        Parts.Add(new Pos(0, 1));
//        Parts.Add(new Pos(0, 2));
//        Parts.Add(new Pos(0, 3));

//        foreach (var item in Parts)
//        {
//            item.Y += r;
//            item.X += 2;
//        }


//    }
//}

//class P5 : Piece
//{
//    public P5(int r)
//    {
//        Height = 2;
//        r -= Height;

//        Parts.Add(new Pos(0, 0));
//        Parts.Add(new Pos(1, 0));
//        Parts.Add(new Pos(0, 1));
//        Parts.Add(new Pos(1, 1));

//        foreach (var item in Parts)
//        {
//            item.Y += r;
//            item.X += 2;
//        }


//    }
//}