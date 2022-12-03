using System.Linq;

namespace AOC2022
{
    [Day(1)]
    class Day01 : Solver
    {
        public override object SolveOne()
        {
            return GroupRows().Select(x => x.Sum(y => int.Parse(y))).Max();
        }

        public override object SolveTwo()
        {
            return GroupRows().Select(x => x.Sum(y => int.Parse(y))).OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
