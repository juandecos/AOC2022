using System.Linq;

namespace AOC2022
{
    [Day(2)]
    class Day02 : Solver
    {
        public override object SolveOne()
        {
            return Rows.Sum(x =>
            {
                (int player1, int player2) = Parse(x);
                return Score(player1, player2);
            });
        }

        public override object SolveTwo()
        {
            return Rows.Sum(x =>
            {
                (int player1, int neededResult) = Parse(x);
                int player2 = neededResult == 0 ? Inferior(player1) : neededResult == 1 ? player1 : Superior(player1);
                return Score(player1, player2);
            });
        }

        private (int, int) Parse(string input) => (input[0] - 'A', input[2] - 'X');
        private int Superior(int p) => (p + 1) % 3;
        private int Inferior(int p) => (p + 2) % 3;
        private int Score(int p1, int p2) => p2 + 1 + (p1 == p2 ? 3 : p1 == Inferior(p2) ? 6 : 0);
    }
}
