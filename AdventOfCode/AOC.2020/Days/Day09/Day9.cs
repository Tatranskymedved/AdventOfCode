using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day9 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day09\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var cl = new CountingList(str);

            //For Part 2 using result of Part 1
            var results = cl.FindMinMaxDigitsForSum(1639024365).ToList();
            var longest = results.First(a => a.Item1 == results.Max(b => b.Item1));
            return longest.Item2.Item1 + longest.Item2.Item2;

            //Part 1 , result: 1639024365
            //for (int i = 25; i < cl.Count; i++)
            //{
            //    var number = cl[i];
            //    if (!cl.ContainsNumber(number))
            //        return number;

            //    cl.Next();
            //}

            //var values = str.Select();
            //return 0;
        }
    }

    class CountingList : List<double>
    {
        public int PreambleSize { get; set; } = 25;
        public int Index { get; set; } = 25;
        public List<Tuple<double, double, double>> Calculations = new List<Tuple<double, double, double>>();

        public bool ContainsNumber(double x) => Calculations.Any(a => a.Item3 == x);
        public IEnumerable<Tuple<int, Tuple<double, double>>> FindMinMaxDigitsForSum(double sum)
        {
            for (int i = 0; i < this.Count; i++)
            {
                double sumSoFar = 0;
                double smallest = double.MaxValue;
                double maximum = double.MinValue;
                for (int j = i; j < this.Count; j++)
                {
                    var curr = this[j];

                    sumSoFar += this[j];
                    if (sumSoFar > sum) break; //we are too far, result not found, not consecutive

                    if (curr < smallest) smallest = curr;
                    if (curr > maximum) maximum = curr;

                    if (sumSoFar == sum)
                    {
                        yield return new Tuple<int, Tuple<double, double>>(j - i + 1, new Tuple<double, double>(smallest, maximum));
                    }
                }
            }
        }

        public CountingList(string[] values)
        {
            this.AddRange(values.Select(a => Convert.ToDouble(a)));
            CalculateSums();
        }

        private void ClearOldResults()
        {
            Calculations.RemoveRange(0, PreambleSize - 1);
        }

        public void CalculateSums()
        {
            int first = Index - PreambleSize;
            int last = Index;

            for (int i = first; i < last; i++)
            {
                for (int j = first; j < last; j++)
                {
                    if (i == j) continue;

                    var result = new Tuple<double, double, double>(this[i], this[j], this[i] + this[j]);
                    Calculations.Add(result);
                }
            }
        }

        public void Next()
        {
            Index++;
            ClearOldResults();
            CalculateSums();
        }
    }
}
