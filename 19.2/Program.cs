class State
{
    public int ore = 0;
    public int clay = 0;
    public int obsidian = 0;
    public int geode = 0;

    public int oreCollectors = 1;
    public int clayCollectors = 0;
    public int obsidianCollectors = 0;
    public int geodeCollectors = 0;

    public int oreCollectorsBuilding = 0;
    public int clayCollectorsBuilding = 0;
    public int obsidianCollectorsBuilding = 0;
    public int geodeCollectorsBuilding = 0;

    public void Collect()
    {
        ore += oreCollectors;
        clay += clayCollectors;
        obsidian += obsidianCollectors;
        geode += geodeCollectors;

        oreCollectors += oreCollectorsBuilding;
        clayCollectors += clayCollectorsBuilding;
        obsidianCollectors += obsidianCollectorsBuilding;
        geodeCollectors += geodeCollectorsBuilding;

        oreCollectorsBuilding = 0;
        clayCollectorsBuilding = 0;
        obsidianCollectorsBuilding = 0;
        geodeCollectorsBuilding = 0;
    }
}

class Program
{
    static int oreCollectorOreCost;
    static int clayCollectorOreCost;

    static int obsidianCollectorOreCost;
    static int obsidianCollectorClayCost;

    static int geodeCollectorOreCost;
    static int geodeCollectorObsidianCost;

    static List<int> ql = new List<int>();

    static void Main()
    {
        var lines = File.ReadAllLines("in.txt");

        int bp = 1;

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

            var minutes = new Dictionary<int, List<State>>();
            minutes.Add(0, new List<State>()
            { new State()
                {
                    ore = 0,
                    clay = 0,
                    obsidian = 0,
                    geode = 0,
                    oreCollectors = 1,
                    clayCollectors = 0,
                    obsidianCollectors = 0,
                    geodeCollectors = 0
                }
            });


            for (int m = 0; m < 32; m++)
            {
                var states = minutes[m];

                Console.WriteLine($"{DateTime.Now}: {bp}:{m}");

                List<State> ml = new List<State>();

                foreach (var s in states)
                {
                    List<State> list = GetNewStates(s);

                    foreach (var l in list)
                    {
                        l.Collect();
                        ml.Add(l);
                    }
                }

                // Extremt magiskt gissad siffra för att få rimlig culling-effekt:
                // Deleta allt som laggar bakom geode max med 4 enheter...
                int gm = ml.Max(f => f.geode);

                if (gm > 4)
                    ml = ml.Where(f => f.geode > gm - 4).ToList();


                minutes.Add(m + 1, ml.DistinctBy(f => new { f.ore, f.clay, f.obsidian, f.geode, f.oreCollectors, f.clayCollectors, f.obsidianCollectors, f.geodeCollectors }).ToList());
            }

            int max = 0;
            var z = minutes[32];
            foreach (var x in z)
            {
                if (x.geode > max) max = x.geode;
            }

            Console.WriteLine($"{bp}: {max} -> QL: {bp * max}");
            ql.Add(max);
            bp++;
        }

        Console.WriteLine(ql[0] * ql[1] * ql[2]);
        Console.ReadKey();
    }

    private static List<State> GetNewStates(State s)
    {
        var list = new List<State>();

        if (s.ore >= geodeCollectorOreCost && s.obsidian >= geodeCollectorObsidianCost)
        {
            var newState1 = new State()
            {
                clay = s.clay,
                ore = s.ore - geodeCollectorOreCost,
                obsidian = s.obsidian - geodeCollectorObsidianCost,
                geode = s.geode,

                clayCollectors = s.clayCollectors,
                geodeCollectors = s.geodeCollectors,
                obsidianCollectors = s.obsidianCollectors,
                oreCollectors = s.oreCollectors,

                geodeCollectorsBuilding = 1
            };

            list.Add(newState1);
        }

        if (s.ore >= obsidianCollectorOreCost && s.clay >= obsidianCollectorClayCost)
        {
            var newState2 = new State()
            {
                clay = s.clay - obsidianCollectorClayCost,
                ore = s.ore - obsidianCollectorOreCost,
                obsidian = s.obsidian,
                geode = s.geode,

                clayCollectors = s.clayCollectors,
                geodeCollectors = s.geodeCollectors,
                obsidianCollectors = s.obsidianCollectors,
                oreCollectors = s.oreCollectors,

                obsidianCollectorsBuilding = 1
            };

            list.Add(newState2);
        }

        if (s.ore >= clayCollectorOreCost)
        {
            var newState3 = new State()
            {
                clay = s.clay,
                ore = s.ore - clayCollectorOreCost,
                obsidian = s.obsidian,
                geode = s.geode,

                clayCollectors = s.clayCollectors,
                geodeCollectors = s.geodeCollectors,
                obsidianCollectors = s.obsidianCollectors,
                oreCollectors = s.oreCollectors,

                clayCollectorsBuilding = 1
            };

            list.Add(newState3);
        }

        if (s.ore >= oreCollectorOreCost)
        {
            var newState4 = new State()
            {
                ore = s.ore - oreCollectorOreCost,
                clay = s.clay,
                obsidian = s.obsidian,
                geode = s.geode,

                oreCollectors = s.oreCollectors,
                clayCollectors = s.clayCollectors,
                geodeCollectors = s.geodeCollectors,
                obsidianCollectors = s.obsidianCollectors,

                oreCollectorsBuilding = 1


            };

            list.Add(newState4);
        }

        var newState5 = new State()
        {
            ore = s.ore,
            clay = s.clay,
            obsidian = s.obsidian,
            geode = s.geode,

            oreCollectors = s.oreCollectors,
            clayCollectors = s.clayCollectors,
            geodeCollectors = s.geodeCollectors,
            obsidianCollectors = s.obsidianCollectors,
        };

        list.Add(newState5);


        return list;
    }
}

