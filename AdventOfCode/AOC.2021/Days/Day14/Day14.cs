using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day14 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day14\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };

            var values = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return row;
            });

            return 0;
        }
    }
}
