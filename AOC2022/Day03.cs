using System.Linq;

namespace AOC2022
{
    [Day(3)]
    class Day03 : Solver
    {
        public override object SolveOne()
        {
            return Rows.Sum(row =>
            {
                var ruck1 = row.Substring(0, row.Length / 2);
                var ruck2 = row.Substring(row.Length / 2);
                var overlap = ruck1.First(x => ruck2.Contains(x));
                return Priority(overlap);
            });
        }

        public override object SolveTwo()
        {
            int sum = 0;
            for (int i = 0; i < Rows.Count; i+=3)
            {
                var ruck1 = Rows[i];
                var ruck2 = Rows[i+1];
                var ruck3 = Rows[i+2];
                var overlap = ruck1.First(x => ruck2.Contains(x) && ruck3.Contains(x));
                sum += Priority(overlap);
            }
            return sum;
        }

        private int Priority(char value) => value < 'a' ? (value - 'A' + 27) : (value - 'a' + 1);
    }
}
