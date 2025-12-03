using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Transactions;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AOC._2025.Days
{
    class Day03 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day03\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var listOfJoltBanks = new List<List<double>>();
            var twoJolts = new List<double>();

            foreach (var line in str)
            {
                var joltBank = new List<double>();

                for (int i = 0; i < line.Length; i++)
                {
                    char digitAsChar = line[i];
                    double digit = Convert.ToDouble(digitAsChar.ToString());
                    joltBank.Add(digit);
                }
                listOfJoltBanks.Add(joltBank);

                twoJolts.Add(RecurSearch(joltBank, 12));

                // First naive solution:

                //var maxVal = joltBank.Max();
                //var maxIndex = joltBank.IndexOf(maxVal);

                //if (maxIndex == joltBank.Count() - 1) //Is last element, we must search for any prev. (must be lower)
                //{
                //    maxVal = joltBank.Where(a => a != maxVal).Max();
                //    maxIndex = joltBank.IndexOf(maxVal);
                //}

                //var max2ndVal = joltBank.Skip(maxIndex + 1).Max();

                //twoJolts.Add(Convert.ToDouble(maxVal.ToString() + max2ndVal.ToString()));
            }

            return twoJolts.Sum();


        }
        double RecurSearch(List<double> joltBank, int depth)
        {
            var maxValsNotUsable = new List<double>();

            var maxVal = joltBank.Max();
            var maxIndex = joltBank.IndexOf(maxVal);

            if (depth == 1) return maxVal;

            while (maxIndex >= (joltBank.Count() - depth + 1)) //Is among last elements and we couldn't create a result out of next elements
            {
                maxValsNotUsable.Add(maxVal);

                maxVal = joltBank.Where(a => maxValsNotUsable.Contains(a) == false).Max();
                maxIndex = joltBank.IndexOf(maxVal);
            }

            var res = RecurSearch(joltBank.Skip(maxIndex + 1).ToList(), depth - 1);


            return Convert.ToDouble(maxVal.ToString() + res.ToString());
        }

    }
}
