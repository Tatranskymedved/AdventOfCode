using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC._2021.Days
{
    class Day1 : ADay
    {
        public override string Main_String()
        {
            var path = @".\Days\Day01\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var values = str.Select(a => Convert.ToInt32(a)).ToList();
            int increment = 0, decrement = 0;

            //Part 1
            //CountIncDec(values, ref increment, ref decrement);

            //Part 2
            var sumValues = new List<int>();

            for (int i = 0; i < values.Count - 2; i++)
            {
                sumValues.Add(values[i] + values[i + 1] + values[i + 2]);
            }

            CountIncDec(sumValues, ref increment, ref decrement);
            return $"Increment: {increment}\tDecrement: {decrement}";
        }

        public void CountIncDec(List<int> list, ref int increment, ref int decrement)
        {
            for (int i = 1, j = 0; i < list.Count; i++, j++)
            {
                if (list[i] > list[j])
                    increment++;
                else
                    decrement++;
            }
        }
    }
}
