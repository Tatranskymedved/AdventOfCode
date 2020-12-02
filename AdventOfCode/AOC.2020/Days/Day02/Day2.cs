using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day2 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day02\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };

            var values = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return new Expr()
                {
                    Min = Convert.ToInt32(row[0]),
                    Max = Convert.ToInt32(row[1]),
                    Char = row[2][0],
                    Value = row[3],
                };
            });

            //return values.Count(a => a.IsValidPartOne());
            return values.Count(a => a.IsValidPartTwo());
        }
    }

    internal class Expr
    {
        public char Char { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Value { get; set; }


        public bool IsValidPartOne()
        {
            var cnt = Value.Count(a => a == Char);
            return cnt >= Min && cnt <= Max;
        }

        public bool IsValidPartTwo()
        {
            try
            {
                return (Value[Min - 1] == Char && Value[Max - 1] != Char) ||
                       (Value[Min - 1] != Char && Value[Max - 1] == Char);
            }
            catch { return false; }
        }
    }
}
