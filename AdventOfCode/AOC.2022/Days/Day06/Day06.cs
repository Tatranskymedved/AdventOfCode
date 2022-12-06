using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day06 : ADay
    {
        public override double Main_Double()
        {
            var testString = new List<string>()
            {
                "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
                "bvwbjplbgvbhsrlpgdmjqwftvncz",
                "nppdvjthqldpwncqszvftbrmjlhg",
                "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
                "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
            };

            var path = @".\Days\Day06\input.txt";

            var str = System.IO.File.ReadAllText(path);

            return findPosOfNthDifferentCharacter(str, 14);
            //return findPosOfNthDifferentCharacter(testString[4], 14);
        }

        public int findPosOfNthDifferentCharacter(string text, int distinctCount)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var substring = text.Substring(i, distinctCount);
                if (substring.Distinct().Count() == distinctCount)
                    return i + distinctCount;
            }

            return -1;
        }
    }

}
