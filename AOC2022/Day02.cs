using System.Linq;

namespace AOC2022
{
    [Day(2)]
    class Day02 : Solver
    {
        public override object SolveOne()
        {
            return Rows
                .Select(x => Parse(x))
                .Sum(inputs => Score(inputs[0], inputs[1]));
        }

        public override object SolveTwo()
        {
            return Rows
                .Select(x => Parse(x))
                .Sum(inputs => Score(inputs[0], GetRequiredMove(inputs[0], inputs[1])));
        }

        private int[] Parse(string input) => new int[] { input[0] - 'A', input[2] - 'X' };
        private int Superior(int move) => (move + 1) % 3;
        private int Inferior(int move) => (move + 2) % 3;
        private int Score(int player1, int player2) => player2 + 1 + (player1 == player2 ? 3 : player1 == Inferior(player2) ? 6 : 0);
        private int GetRequiredMove(int player1, int result) => result == 0 ? Inferior(player1) : result == 1 ? player1 : Superior(player1);
    }
}
