using AOC.Common.Days;
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
            var charsToRemove = new char[] { ' ', ':', '-', '>' };

            //var pe = new PolymerExtender(str, charsToRemove);
            //Console.WriteLine(pe.Formula);
            //return pe.MakeStepsAndSumLeastAndMostCommon(10);

            var pc = new PolymerCounter(str, charsToRemove);
            return pc.MakeStepsAndSumLeastAndMostCommon(40);
        }
    }

    /// <summary>
    /// Part 2
    /// </summary>
    class PolymerCounter
    {
        public Dictionary<string, double> cntr = new Dictionary<string, double>();
        public Dictionary<string, string> rules = new Dictionary<string, string>();
        public char LastChar { get; set; }

        public PolymerCounter(string[] lines, char[] charsToRemove)
        {
            var formula = lines.First();
            LastChar = formula.Last();

            lines.Skip(1).ToList().ForEach(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                if (row.Length != 2) return;

                rules.Add(row[0], row[1]);
                cntr.Add(row[0], 0);
            });

            for (int i = 0; i < formula.Length - 1; i++)
            {
                var key = formula.Substring(i, 2);
                if (cntr.TryGetValue(key, out var val))
                {
                    cntr[key] = val + 1;
                }
                else
                {
                    cntr[key] = 1;
                }
            }
        }

        public double MakeStepsAndSumLeastAndMostCommon(int steps)
        {
            var tempDict = new Dictionary<string, double>();
            for (int i = 0; i < steps; i++)
            {
                tempDict.Clear();
                foreach (var rule in rules)
                {
                    var newKey = rule.Key[0] + rule.Value;
                    var newKey2 = rule.Value + rule.Key[1];

                    if (tempDict.TryGetValue(newKey, out var value))
                    {
                        tempDict[newKey] = value + cntr[rule.Key];
                    }
                    else
                    {
                        if (cntr[rule.Key] > 0)
                            tempDict[newKey] = cntr[rule.Key];
                    }

                    if (tempDict.TryGetValue(newKey2, out var value2))
                    {
                        tempDict[newKey2] = value2 + cntr[rule.Key];
                    }
                    else
                    {
                        if (cntr[rule.Key] > 0)
                            tempDict[newKey2] = cntr[rule.Key];
                    }

                    if (tempDict.TryGetValue(rule.Key, out var oldValue))
                    {
                        tempDict[rule.Key] = oldValue - cntr[rule.Key];
                    }
                    else
                    {
                        tempDict[rule.Key] = -1 * cntr[rule.Key];
                    }


                }
                var keys = cntr.Keys.ToList();
                foreach (var key in keys)
                {
                    if (tempDict.ContainsKey(key))
                        cntr[key] = cntr[key] + tempDict[key];
                }
            }

            var resultDict = new Dictionary<char, double>();
            foreach (var cnt in cntr)
            {
                var newKey = cnt.Key[0];

                if (resultDict.TryGetValue(newKey, out var value))
                {
                    resultDict[newKey] = value + cnt.Value;
                }
                else
                {
                    if (cnt.Value > 0)
                        resultDict[newKey] = cnt.Value;
                }
            }

            resultDict[LastChar]++;
            var max = resultDict.Max(a => a.Value);
            var min = resultDict.Min(a => a.Value);

            return max - min;
        }
    }


    /// <summary>
    /// Part 1
    /// </summary>
    class PolymerExtender
    {
        public string Formula { get; set; }
        public Dictionary<string, string> rules = new Dictionary<string, string>();

        public PolymerExtender(string[] lines, char[] charsToRemove)
        {
            Formula = lines.First();

            lines.Skip(1).ToList().ForEach(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                if (row.Length != 2) return;

                rules.Add(row[0], row[1]);
            });
        }


        public double MakeStepsAndSumLeastAndMostCommon(int stepCnt)
        {
            Dictionary<int, string> mapPositionToNewValue = new Dictionary<int, string>();
            for (int i = 0; i < stepCnt; i++)
            {
                mapPositionToNewValue.Clear();
                Console.WriteLine(i);

                for (int n = 0; n < Formula.Length - 1; n++)
                {
                    if (rules.TryGetValue(Formula.Substring(n, 2), out var newStr))
                    {
                        mapPositionToNewValue[n + 1] = newStr;
                    };
                }

                foreach (var item in mapPositionToNewValue.OrderByDescending(a => a.Key))
                {
                    Formula = Formula.Insert(item.Key, item.Value);
                }
                //Console.WriteLine(Formula);
            }

            var cnts = Formula.GroupBy(a => a).Select(a => a.LongCount());

            return cnts.Max() - cnts.Min();
        }
    }
}
