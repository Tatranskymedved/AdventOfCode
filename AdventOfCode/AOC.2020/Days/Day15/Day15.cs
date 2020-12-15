using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day15 : ADay
    {
        public override int Main_Int32()
        {
            string input = "15,5,1,4,7,0";
            //string input = "0,3,6";
            var spokenDict = new OrderedDictionary();
            var spokenNumbers = input.Split(',').Select((a, i) => new KeyValuePair<int, List<int>>(Convert.ToInt32(a), new List<int>(new int[] { i }))).ToList(); //.ToDictionary(a => a.Key, a => a.Value);
            spokenNumbers.ForEach(a => spokenDict.Add(a.Key, a.Value));

            var output = spokenNumbers.Select(a => a.Key).ToList();
            int max = 2020;
            int lastNumber = spokenNumbers.Last().Key;
            for (int i = spokenDict.Count; i < max; i++)
            {
                if (spokenDict.Contains(lastNumber) && (spokenDict[(object)lastNumber] as List<int>).Where(a => a != i-1).Any())
                    lastNumber = i - (spokenDict[(object)lastNumber] as List<int>).SkipLast(1).Last() - 1;
                else
                    lastNumber = 0;

                if (spokenDict.Contains(lastNumber))
                {
                    var list = spokenDict[(object)lastNumber] as List<int>;
                    list.Add(i);
                }
                else
                    spokenDict.Add(lastNumber, new List<int>(new int[] { i }));

                //Console.WriteLine(lastNumber);
                output.Add(lastNumber);
            }
            
            return lastNumber;
        }
    }
}
