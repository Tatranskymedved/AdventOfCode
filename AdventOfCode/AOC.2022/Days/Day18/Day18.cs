using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC._2022.Days
{
    class Day18 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day18\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            //var values = str.Select(line =>
            //{
            //    return new Snail(line);
            //});

            //new Snail("[1,2]");
            //new Snail("[[1,2],3]", 0);
            //new Snail("[1,[2,3]]", 0);
            var s = new Snail("[[1,9],[8,5]]", 0);
            //new Snail("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]");

            return 0;
        }
    }

    class Snail
    {
        public int? Number { get; set; }
        static Regex regJustNumbers = new Regex(@"^(?<x>\d),(?<y>\d)$", RegexOptions.Singleline);
        static Regex regNumberRight = new Regex(@"(?<str>.*),(?<y>\d)$", RegexOptions.Singleline);
        static Regex regNumberLeft = new Regex(@"^(?<x>\d),(?<str>.*)", RegexOptions.Singleline);
        Snail left;
        Snail right;
        Snail child;

        public int X { get; set; }
        public int Y { get; set; }
        public int Depth { get; set; }

        public Snail(string line, int depth)
        {
            //Depth = depth;
            //if (line.Length == 1)
            //{
            //    Number = Convert.ToInt32(line[0].ToString());
            //    return;
            //}

            //var updLine = line.Remove(0, 1);
            //updLine = updLine.Remove(updLine.Length - 1, 1);

            //if(updLine.Length)

            //var resJustNumbers = regJustNumbers.Match(updLine);
            //var resNumberRight = regNumberRight.Match(updLine);
            //var resNumberLeft = regNumberLeft.Match(updLine);


            //if (updLine[0] == '[' && updLine[updLine.Length - 1] == ']')
            //{
            //    child = new Snail(updLine, depth + 1);
            //}
            //else if (resJustNumbers.Success)
            //{
            //    X = Convert.ToInt32(resJustNumbers.Groups["x"].Value);
            //    Y = Convert.ToInt32(resJustNumbers.Groups["y"].Value);
            //}
            //else if (resNumberRight.Success)
            //{
            //    left = new Snail(resNumberRight.Groups["str"].Value, depth + 1);
            //    Y = Convert.ToInt32(resNumberRight.Groups["y"].Value);
            //}
            //else if (resNumberLeft.Success)
            //{
            //    X = Convert.ToInt32(resNumberLeft.Groups["x"].Value);
            //    right = new Snail(resNumberLeft.Groups["str"].Value, depth + 1);
            //}
            //else
            //{
            //    ;
            //}
        }
    }
}
