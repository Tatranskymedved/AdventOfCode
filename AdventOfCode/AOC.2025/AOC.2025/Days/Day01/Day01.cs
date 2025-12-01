using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Transactions;

namespace AOC._2025.Days
{
    class Day01 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day01\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var su = new SafeUnlocker(str);
            su.Solve();

            //Part 1
            return su.HowManyTimesWas0;

            //Part 2

            return su.HowManyTimesWentThrough0;
        }

        class SafeUnlocker
        {
            public record Instruction(bool IsLeft, int StepSize);

            public int CurrentValue { get; set; } = 50;
            public int HowManyTimesWas0 { get; set; } = 0;
            public int HowManyTimesWentThrough0 { get; set; } = 0;

            public List<Instruction> Instructions { get; set; } = new List<Instruction>();

            public SafeUnlocker(IEnumerable<string> lines)
            {
                foreach (var line in lines)
                {
                    int amount = Convert.ToInt32(line.Substring(1));

                    for (int i = 0; i < amount; i++)
                    {
                        Instructions.Add(new Instruction(
                        IsLeft: line[0].Equals('L'),
                        StepSize: 1
                        ));
                    }

                    //Instructions.Add()

                    //Instructions.Add(new Instruction(
                    //    IsLeft: line[0].Equals('L'),
                    //    StepSize: Convert.ToInt32(line.Substring(1))
                    //    ));
                }
            }

            public void Solve()
            {
                foreach (var instruction in Instructions)
                {
                    int stepSize = instruction.StepSize * (instruction.IsLeft ? -1 : 1);
                    //Console.WriteLine($"{CurrentValue} + {stepSize} | {CurrentValue + stepSize}");
                    CurrentValue += stepSize;

                    while (CurrentValue > 99)
                    {
                        CurrentValue += -100;
                        HowManyTimesWentThrough0++;
                    }
                    while (CurrentValue < 0)
                    {
                        CurrentValue += 100;
                        HowManyTimesWentThrough0++;
                    }


                    if (CurrentValue == 0) HowManyTimesWas0 += 1;

                    //Console.WriteLine($"{HowManyTimesWentThrough0}");
                }

            }
        }

    }
}
