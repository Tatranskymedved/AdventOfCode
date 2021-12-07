using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day7 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day07\input.txt";
            //var path = @".\Days\Day07\input2.txt";


            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', ',' };

            int min = 100, max = 100;
            var values = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries).Select(a => Convert.ToInt32(a));
                foreach (var item in row)
                {
                    min = Math.Min(min, item);
                    max = Math.Max(max, item);
                }
                return row;
            }).First();
            GenerateTriangleNumber(max + 1);

            var minFuel = int.MaxValue;
            for (int i = min; i < max; i++)
            {
                int fuel = values.Sum(a => triangleArray[Math.Abs(i - a)]);
                minFuel = Math.Min(fuel, minFuel);
            }

            return minFuel;
        }

        private void GenerateTriangleNumber(int max)
        {
            triangleArray = new int[max];
            triangleArray[0] = 0;
            for (int i = 1; i < max; i++)
            {
                triangleArray[i] = triangleArray[i - 1] + i;
            }
        }

        private static int[] triangleArray = new int[0];
    }
}