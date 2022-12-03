class Program
{
    static void Main()
    {
        string[] lines = File.ReadLines("in.txt").ToArray();

        var rounds = new List<Round>();

        foreach (var item in lines)
        {
            var r = item.Split(' ');

            var round = new Round();
            round.SetOpponent(r[0][0]);
            round.SetMe(r[1][0]);
            rounds.Add(round);
        }

        int s = rounds.Sum(f => f.Score());

        Console.WriteLine(s);
        Console.ReadKey();
    }


    enum Hand
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    class Round
    {
        public Hand Opponent { get; set; }
        public Hand Me { get; set; }

        public void SetOpponent(char c)
        {
            switch (c)
            {
                case 'A': Opponent = Hand.Rock; break;
                case 'B': Opponent = Hand.Paper; break;
                case 'C': Opponent = Hand.Scissors; break;

                default: break;
            }
        }

        public void SetMe(char c)
        {
            switch (c)
            {
                case 'X': Me = Hand.Rock; break;
                case 'Y': Me = Hand.Paper; break;
                case 'Z': Me = Hand.Scissors; break;
            }
        }

        private Hand GetWinningHand(Hand i)
        {
            switch (i)
            {
                case Hand.Rock: return Hand.Paper;
                case Hand.Paper: return Hand.Scissors;
                case Hand.Scissors: return Hand.Rock;
                default: throw new Exception();
            }
        }

        public int Score()
        {
            int score = (int)Me;

            if (Opponent == Me)
            {
                score += 3;
            }
            else if (Me == GetWinningHand(Opponent))
            {
                score += 6;
            }

            return score;
        }
    }
}

