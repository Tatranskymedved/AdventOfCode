using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day8 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day08\input.txt";
            //var path = @".\Days\Day08\input2.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemoveLR = new char[] { '|' };
            var charsToRemove = new char[] { ' ', ':', '-' };

            var values = str.Select(line =>
            {
                var leftRight = line.Split(charsToRemoveLR, StringSplitOptions.RemoveEmptyEntries);
                var left = leftRight[0].Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                var right = leftRight[1].Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return new SegmentObservedRow(left, right);
            });

            //return values.Sum(a => a.ResultArray.Count(b => b == 1 || b == 4 || b == 7 || b == 8));

            //Part 2
            return values.Sum(a => a.ResultNumber);
        }
    }

    class SegmentObservedRow
    {
        public string Zero { get; private set; }
        public string One { get; private set; }
        public string Two { get; private set; }
        public string Three { get; private set; }
        public string Four { get; private set; }
        public string Five { get; private set; }
        public string Six { get; private set; }
        public string Seven { get; private set; }
        public string Eight { get; private set; }
        public string Nine { get; private set; }

        public int[] ResultArray = new int[4] { -1, -1, -1, -1 };
        public int ResultNumber { get; set; } = 0;

        private Dictionary<int, string> dict = new Dictionary<int, string>();

        public SegmentObservedRow(string[] left, string[] right)
        {
            Parse(left);
            ParseResult(right);
        }

        private void Parse(string[] left)
        {
            One = left.Where(a => a.Count() == 2).FirstOrDefault();
            Four = left.Where(a => a.Count() == 4).FirstOrDefault();
            Seven = left.Where(a => a.Count() == 3).FirstOrDefault();
            Eight = "abcdefg";

            //6 parts - 0,6,9
            Six = left.Where(a => a.Count() == 6).Single(a => One.All(b => a.Contains(b)) == false);
            char rightUp = Eight.First(a => Six.Contains(a) == false);
            string leftUpAndMiddle = string.Concat(Four.Where(a => One.Contains(a) == false));
            Nine = left.Where(a => a.Count() == 6).First(a => (a.All(b => Six.Contains(b)) == false) && leftUpAndMiddle.All(b => a.Contains(b)));
            Zero = left.Where(a => a.Count() == 6).First(a => (a.All(b => Nine.Contains(b)) == false) && (a.All(b => Six.Contains(b)) == false));

            //5 parts - 2,3,5
            Three = left.Where(a => a.Count() == 5).Single(a => One.All(b => a.Contains(b)));
            Two = left.Where(a => a.Count() == 5).Single(a => a.Contains(rightUp) && (a.All(b => Three.Contains(b)) == false));
            Five = left.Where(a => a.Count() == 5).First(a => (a.All(b => Two.Contains(b)) == false) && (a.All(b => Three.Contains(b)) == false));
        }


        private void ParseResult(string[] right)
        {
            for (int i = 0; i < right.Length; i++)
            {
                var str = right[i];
                if (str.Length == One.Length && str.All(b => One.Contains(b))) ResultArray[i] = 1;
                if (str.Length == Four.Length && str.All(b => Four.Contains(b))) ResultArray[i] = 4;
                if (str.Length == Seven.Length && str.All(b => Seven.Contains(b))) ResultArray[i] = 7;
                if (str.Length == Eight.Length && str.All(b => Eight.Contains(b))) ResultArray[i] = 8;

                //Part 2
                //6 parts - 0,6,9
                if (str.Length == Zero.Length && str.All(b => Zero.Contains(b))) ResultArray[i] = 0;
                if (str.Length == Six.Length && str.All(b => Six.Contains(b))) ResultArray[i] = 6;
                if (str.Length == Nine.Length && str.All(b => Nine.Contains(b))) ResultArray[i] = 9;

                //5 parts - 2,3,5
                if (str.Length == Two.Length && str.All(b => Two.Contains(b))) ResultArray[i] = 2;
                if (str.Length == Three.Length && str.All(b => Three.Contains(b))) ResultArray[i] = 3;
                if (str.Length == Five.Length && str.All(b => Five.Contains(b))) ResultArray[i] = 5;
            }
            if (ResultArray.Any(a => a < 0)) throw new Exception();

            //Part 2
            ResultNumber = Convert.ToInt32(ResultArray[0].ToString() + ResultArray[1].ToString() + ResultArray[2].ToString() + ResultArray[3].ToString());
        }
    }
}
