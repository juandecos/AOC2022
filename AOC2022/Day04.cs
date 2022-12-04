using System.Linq;

namespace AOC2022
{
    [Day(4)]
    class Day04 : Solver
    {
        public class Range
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public bool Contains(Range other) => other.Min >= Min && other.Max <= Max;
            public bool Overlaps(Range other) => other.Min <= Max && other.Max >= Min;
            public Range(string input)
            {
                var split = input.Split('-');
                Min = int.Parse(split[0]);
                Max = int.Parse(split[1]);
            }
        }

        public override object SolveOne()
        {
            return Rows
                .Select(x => Parse(x))
                .Count(elves => elves[0].Contains(elves[1]) || elves[1].Contains(elves[0]));
        }

        public override object SolveTwo()
        {
            return Rows
                .Select(x => Parse(x))
                .Count(elves => elves[0].Overlaps(elves[1]));
        }

        private Range[] Parse(string input) => input.Split(',').Select(x => new Range(x)).ToArray();
    }
}
