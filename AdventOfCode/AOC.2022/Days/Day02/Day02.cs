using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day02 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day02\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };

            int horizontalPosition = 0, depth = 0, aim = 0;

            var values = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return row;
            }).ToList();

            values.ForEach(a =>
            {
                int val = Int32.Parse(a[1]);
                switch (a[0])
                {
                    case "forward":
                        horizontalPosition += val;
                        depth += aim * val;
                        break;
                    case "down":
                        aim += val;
                        break;
                    case "up":
                        aim -= val;
                        break;
                    default:
                        throw new Exception("unknown");
                }
            });

            return horizontalPosition * depth;
        }
    }
}
