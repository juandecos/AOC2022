using System.Linq;

namespace AOC2022
{
    [Day(1)]
    class Day01 : Solver
    {
        public override object SolveOne()
        {
            return GroupIntRows().Select(x => x.Sum()).Max();
        }

        public override object SolveTwo()
        {
            return GroupIntRows().Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
