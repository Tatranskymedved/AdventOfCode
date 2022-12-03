using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day03 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day03\input.txt";
            //var path = @".\Days\Day03\myInput.txt";

            var str = System.IO.File.ReadAllLines(path).Select(a => new Rucksack() { Value = a }).ToList();

            //Part 1
            //return str.Sum(a => a.Priority);

            //Part 2
            var rucksackGroups = str.Select((x, i) => new { x, i })
                    .GroupBy(x => x.i / 3)
                    .Select(g => g.ToList())
                    .Select(g => new RucksackGroup { Elf1 = g[0].x, Elf2 = g[1].x, Elf3 = g[2].x });

            return rucksackGroups.Sum(b => b.Priority);
        }

        class RucksackGroup
        {
            public Rucksack Elf1 { get; set; }
            public Rucksack Elf2 { get; set; }
            public Rucksack Elf3 { get; set; }
            public string PrioS => FindCommonCharAsString(Elf1.Value, Elf2.Value, Elf3.Value);
            public int Priority => AOC.Common.AlphabetHelpers.GetAlphabetForCharAsString(PrioS);

            public string FindCommonCharAsString(string a, string b, string c)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < b.Length; j++)
                    {
                        for (int k = 0; k < c.Length; k++)
                        {
                            if (a[i] == b[j] && b[j] == c[k])
                                return a[i].ToString();
                        }
                    }
                }

                return "";
            }
        }

        class Rucksack
        {
            public string Value { get; set; }
            public string Item1 => Value.Substring(0, Value.Length / 2);
            public string Item2 => Value.Substring(Value.Length / 2);
            public string PrioS => FindCommonCharAsString(Item1, Item2);
            public int Priority => AOC.Common.AlphabetHelpers.GetAlphabetForCharAsString(PrioS);

            public string FindCommonCharAsString(string a, string b)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < b.Length; j++)
                    {
                        if (a[i] == b[j])
                            return a[i].ToString();
                    }
                }

                return "";
            }
        }
    }
}
