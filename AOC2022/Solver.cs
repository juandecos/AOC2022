using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
    public abstract class Solver
    {
        public List<string> Rows { get; set; }
        public List<long> LongRows { get; set; }
        public List<int> IntRows { get; set; }

        public Solver()
        {
            DayAttribute attribute = (DayAttribute)Attribute.GetCustomAttribute(GetType(), typeof(DayAttribute));
            string input = Properties.Resources.ResourceManager.GetString("Input" + attribute.Day.ToString().PadLeft(2, '0'));
            Rows = input.Split("\r\n").ToList();
            LongRows = Rows.ConvertAll(x => long.TryParse(x, out long n) ? n : 0);
            IntRows = Rows.ConvertAll(x => int.TryParse(x, out int n) ? n : 0);
        }

        public abstract object SolveOne();
        public abstract object SolveTwo();

        public void SolveAndPrintOne()
        {
            Console.WriteLine(SolveOne().ToString());
            Console.WriteLine("-------- DONE --------");
        }

        public void SolveAndPrintTwo()
        {
            Console.WriteLine(SolveTwo().ToString());
            Console.WriteLine("-------- DONE --------");
        }

        // Form groups based on a number of rows per group
        public IEnumerable<List<T>> GroupRows<T>(Func<string, T> parser, int groupSize)
        {
            var group = new List<T>();
            int count = 0;
            foreach (var row in Rows)
            {
                if (count != groupSize)
                {
                    group.Add(parser(row));
                    count++;
                    continue;
                }
                yield return group.ConvertAll(x => x);
                group.Clear();
                group.Add(parser(row));
                count = 1;
            }
            yield return group;
        }

        // Form rows based on empty line delimiting
        public IEnumerable<List<T>> GroupRows<T>(Func<string, T> parser)
        {
            var group = new List<T>();
            foreach (var row in Rows)
            {
                if (row.Length != 0)
                {
                    group.Add(parser(row));
                    continue;
                }
                yield return group.ConvertAll(x => x);
                group.Clear();
            }
            yield return group;
        }

        public IEnumerable<List<string>> GroupRows(int groupSize)
        {
            return GroupRows(x => x, groupSize);
        }

        public IEnumerable<List<string>> GroupRows()
        {
            return GroupRows(x => x);
        }
    }

    public static class SolverExtensions
    {
        public static string[] Split(this string input, string delimiter)
        {
            return input.Split(new string[] { delimiter }, StringSplitOptions.None);
        }

        public static string[] Split(this string input, int chunkCount)
        {
            string[] chunks = new string[chunkCount];
            int chunkLength = input.Length / chunkCount;
            for (int i = 0; i < chunkCount; i++)
            {
                chunks[i] = input.Substring(i * chunkLength, chunkLength);
            }
            return chunks;
        }

        public static IEnumerable<TResult> IntersectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return new TResult[0];

                var ret = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    ret = ret.Intersect(selector(enumerator.Current));
                }

                return ret;
            }
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = new TValue();
                dictionary.Add(key, value);
            }
            return value;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DayAttribute : Attribute
    {
        public int Day { get; set; }

        public DayAttribute(int day)
        {
            Day = day;
        }
    }
}
