using System.Linq;

namespace AOC2022
{
    [Day(3)]
    class Day03 : Solver
    {
        public override object SolveOne()
        {
            return Rows
                .Select(x => x.Split(2))
                .Sum(compartments => Priority(compartments[0].Intersect(compartments[1]).First()));
        }

        public override object SolveTwo()
        {
            return GroupRows(3)
                .Sum(rucksacks => Priority(rucksacks[0].Intersect(rucksacks[1]).Intersect(rucksacks[2]).First()));
        }

        private int Priority(char value) => value < 'a' ? (value - 'A' + 27) : (value - 'a' + 1);
    }
}
