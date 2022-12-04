using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC._2022.Days
{
    class Day04 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day04\input.txt";

            var lines = System.IO.File.ReadAllLines(path);
            var elves = lines.Select(a => new ElvenPair(a.Split(','))).ToList();

            //return elves.Count(a => a.DoesFullyContainOneTheOther);
            return elves.Count(a => a.Overlaps);
        }

        class ElvenPair
        {
            public HashSet<int> Elf1 { get; set; }
            public HashSet<int> Elf2 { get; set; }
            public bool DoesElf1FullyContainElf2 => Elf2.IsSubsetOf(Elf1);
            public bool DoesElf2FullyContainElf1 => Elf1.IsSubsetOf(Elf2);
            public bool DoesFullyContainOneTheOther => DoesElf1FullyContainElf2 || DoesElf2FullyContainElf1;
            public bool Overlaps => Elf1.Overlaps(Elf2);

            public ElvenPair() { }
            public ElvenPair(string[] elves)
            {
                Elf1 = new HashSet<int>(GenerateNumbersFromRange(elves[0]));
                Elf2 = new HashSet<int>(GenerateNumbersFromRange(elves[1]));
            }

            public static IEnumerable<int> GenerateNumbersFromRange(string range)
            {
                var result = new List<int>();

                var r = range.Split('-');
                var start = Convert.ToInt32(r[0]);
                var end = Convert.ToInt32(r[1]);

                for (int i = start; i <= end; i++)
                {
                    result.Add(i);
                }

                return result;
            }
        }
    }
}
