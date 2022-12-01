using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC._2022.Days
{
    class Day01 : ADay
    {
        public override string Main_String()
        {
            var path = @".\Days\Day01\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var elves = new List<Elf>();
            int sum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                var line = str[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    var elf =new Elf() { Calories = sum };
                    elves.Add(elf);
                    sum = 0;
                    continue;
                }
                else
                {
                    sum += Int32.Parse(line);
                }
            }
            if (sum != 0)
            {
                var elf = new Elf() { Calories = sum };
                elves.Add(elf);
            }

            //Part 1
            //return elves.Max(a => a.Calories).ToString();

            //Part 2
            var sortedElves = elves.OrderByDescending(a => a.Calories).ToList();
            return (sortedElves[0].Calories + sortedElves[1].Calories + sortedElves[2].Calories).ToString();
        }

        class Elf
        {
            public int Calories { get; set; } = 0;
        }
    }
}
