using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day05 : ADay
    {
        public override string Main_String()
        {
            var path = @".\Days\Day05\input.txt";
            //var path = @".\Days\Day05\input2.txt";

            var str = System.IO.File.ReadAllLines(path).ToList();
            var emptyLineIndex = str.IndexOf("");

            var sh = new StackHolder(str.Take(emptyLineIndex - 1).ToArray(), str.Skip(emptyLineIndex + 1).ToArray());
            sh.PerformAllSteps(IsPartTwo: true);

            return sh.TopCrates;
        }

        class StackHolder
        {
            Stack<string>[] Stacks = new Stack<string>[11];
            List<Step> Steps = new List<Step>();

            public string TopCrates => string.Join("", Stacks.Select(a => a.Count > 0 ? a.Peek() : ""));

            public StackHolder(string[] lines, string[] steps)
            {
                var charsToRemove = new char[] { ' ', '[', ']' };

                for (int i = 0; i < Stacks.Length; i++)
                {
                    Stacks[i] = new Stack<string>();
                }

                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i];
                    line = line.Replace("    ", "[_]");

                    var split = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < split.Length; j++)
                    {
                        var val = split[j];
                        if (val != "_")
                            Stacks[j + 1].Push(val);
                    }
                }

                Steps = steps.Select(a => new Step(a)).ToList();
            }

            public void PerformAllSteps(bool IsPartTwo = true)
            {
                for (int i = 0; i < Steps.Count; i++)
                {
                    if (IsPartTwo)
                    {
                        Steps[i].PerformStepWithMultipleCrates(Stacks);
                    }
                    else
                    {
                        Steps[i].PerformStepWithSingleCrate(Stacks);
                    }
                }
            }
        }

        class Step
        {
            public string Init { get; set; }
            public int Count { get; set; }
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }

            public Step(string val)
            {
                Init = val;
                val = val.Replace("move ", "");
                val = val.Replace(" from ", " ");
                val = val.Replace(" to ", " ");
                var vals = val.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                Count = Convert.ToInt32(vals[0]);
                StartIndex = Convert.ToInt32(vals[1]);
                EndIndex = Convert.ToInt32(vals[2]);
            }

            public void PerformStepWithSingleCrate(Stack<string>[] stacks)
            {
                for (int i = 0; i < Count; i++)
                {
                    var tmp = stacks[StartIndex].Pop();
                    stacks[EndIndex].Push(tmp);
                }
            }

            public void PerformStepWithMultipleCrates(Stack<string>[] stacks)
            {
                var tmp = new Stack<string>();
                for (int i = 0; i < Count; i++)
                {
                    tmp.Push(stacks[StartIndex].Pop());
                }
                for (int i = 0; i < Count; i++)
                {
                    stacks[EndIndex].Push(tmp.Pop());
                }
            }
        }
    }
}
