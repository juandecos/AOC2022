using System.Linq;

namespace AOC2022
{
    [Day(1)]
    class Day01 : Solver
    {
        public override object SolveOne()
        {
            return GroupRows(x => int.Parse(x))
                .Select(elf => elf.Sum())
                .Max();
        }

        public override object SolveTwo()
        {
            return GroupRows(x => int.Parse(x))
                .Select(elf => elf.Sum())
                .OrderByDescending(elf => elf)
                .Take(3)
                .Sum();
        }
    }
}
