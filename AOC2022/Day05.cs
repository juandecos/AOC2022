using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    [Day(5)]
    class Day05 : Solver
    {
        class Instruction
        {
            public int Amount { get; set; }
            public int From { get; set; }
            public int To { get; set; }
            public Instruction(string input)
            {
                var split = input.Split(" ");
                Amount = int.Parse(split[1]);
                From = int.Parse(split[3]) - 1;
                To = int.Parse(split[5]) - 1;
            }
            public void Apply(Stack<char>[] stacks, bool reverse)
            {
                var boxesMoving = stacks[From].Take(Amount).ToList();
                if (reverse)
                {
                    boxesMoving.Reverse();
                }
                foreach (var box in boxesMoving)
                {
                    stacks[To].Push(box);
                    stacks[From].Pop();
                }
            }
        }

        public override object SolveOne()
        {
            var instructions = ParseInstructions();
            var stacks = ParseStacks();
            instructions.ForEach(x => x.Apply(stacks, false));
            return GetHeads(stacks);
        }

        public override object SolveTwo()
        {
            var instructions = ParseInstructions();
            var stacks = ParseStacks();
            instructions.ForEach(x => x.Apply(stacks, true));
            return GetHeads(stacks);
        }

        private IEnumerable<string> Rotate(IEnumerable<string> input)
        {
            for (int i = 0; i < input.First().Length; i++)
            {
                yield return new string(input.Select(x => x[i]).ToArray());
            }
        }

        private Stack<char>[] ParseStacks()
        {
            var inputRows = Rows
                .Where(x => !x.StartsWith("move") && x.Trim() != "")
                .Select(x => x.Replace('[', ' ').Replace(']', ' '))
                .Reverse()
                .Skip(1);
            return Rotate(inputRows)
                .Where(x => x.Trim() != "")
                .Select(x => new Stack<char>(x.Trim()))
                .ToArray();
        }
        private List<Instruction> ParseInstructions() => Rows.Where(x => x.StartsWith("move")).Select(x => new Instruction(x)).ToList();

        private string GetHeads(Stack<char>[] stacks) => string.Join("", stacks.Select(x => x.Peek()));
    }
}
