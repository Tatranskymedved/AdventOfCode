using AOC.Common.Days;
using System;
using System.Linq;

namespace AOC._2020.Days
{
    class Day1 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day01\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var values = str.Select(a => Convert.ToInt32(a)).ToList();

            //var dict = values.AsParallel().ToDictionary(a => 2020 - a);
            //var result_part1 = dict.FirstOrDefault(a => dict.ContainsKey(a.Value));
            //return result_part1.Key * result_part1.Value;

            for (int x = 0; x < values.Count(); x++)
            {
                var first = values[x];
                if (first >= 2020) continue;

                for (int y = 0; y < values.Count(); y++)
                {
                    var second = values[y];

                    if (first + second >= 2020) continue;
                    if (x == y) continue;

                    for (int z = 0; z < values.Count(); z++)
                    {
                        var third = values[z];

                        if (x == z || y == z) continue;


                        if (first + second + third == 2020) return first * second * third;
                    }
                }
            }

            return 0;
        }
    }
}
