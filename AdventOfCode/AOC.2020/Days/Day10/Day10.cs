using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day10 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day10\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var ac = new AdapterCollection(str);
            // Part 1
            //var result = ac.Find123JoltDifferencesUsingAllAdapters();
            //return result.Item1 * result.Item3;

            ac.Add(0);
            return ac.GetTotalCountOfWaysToArrangeAllAdapters(ac.OrderBy(a=>a));
        }
    }

    class AdapterCollection : List<int>
    {
        public int MyDeviceBuiltinAdapterJoltage => this.Max() + 3;

        public Dictionary<string, long> dict = new Dictionary<string, long>();

        public AdapterCollection(string[] values)
        {
            this.AddRange(values.Select(a => Convert.ToInt32(a)));
        }

        //Couldn't solve this one myself, tried to get the permutation math together but didn't figured out how to.
        //However inspired by https://www.youtube.com/watch?v=Ww3FVxDMZjo&ab_channel=TurkeyDev , he actually uses dictionary as a cache to store already counted results
        //This way we don't have to count everything and store already counted ones. Much improved performance.
        public long GetTotalCountOfWaysToArrangeAllAdapters(IEnumerable<int> inputs)
        {
            if (inputs.Count() == 1) return 1;

            long arrangmentValue = 0;
            int index = 1;
            int current = inputs.First();
            while (inputs.Count() > index && inputs.Skip(index).First() - current < 4)
            {
                var part = inputs.Skip(index);
                string partAsString =  string.Join(',', part);

                if (dict.ContainsKey(partAsString))
                {
                    arrangmentValue += dict[partAsString];
                }
                else
                {
                    long subArrangements = GetTotalCountOfWaysToArrangeAllAdapters(part);
                    dict.Add(partAsString, subArrangements);
                    arrangmentValue += subArrangements;
                }

                index++;
            }
            return arrangmentValue;
        }

        //Recursive slow - all possibilities
        public int GetTotalCountOfWaysToArrangeAllAdapters_Recc()
        {
            int max = MyDeviceBuiltinAdapterJoltage;

            int countOfResult = 0;

            var chainSoFar = new List<int>() { 0 };
            var last = 0;

            var possibles = this.FindAll(a => a > last && a <= (last + 3));
            foreach (var item in possibles)
            {
                RecursiveFnc(item);
            }

            return countOfResult;

            void RecursiveFnc(int lastr)
            {
                if (lastr + 3 == max)
                {
                    countOfResult++;
                    return;
                }

                var possibles = this.FindAll(a => a > lastr && a <= (lastr + 3));
                foreach (var item in possibles)
                {
                    RecursiveFnc(item);
                }
            }
        }

        public Tuple<int, int, int> Find123JoltDifferencesUsingAllAdapters(List<int> list = null)
        {
            if (list == null) list = new List<int>();

            int Difference1JoltCount = 0;
            int Difference2JoltCount = 0;
            int Difference3JoltCount = 0;

            var orderedList = list.OrderBy(a => a).ToList();

            var previousItem = 0;
            for (int i = 0; i < orderedList.Count; i++)
            {
                var currentItem = orderedList[i];
                var difference = currentItem - previousItem;
                switch (difference)
                {
                    case 1:
                        Difference1JoltCount++;
                        break;
                    case 2:
                        Difference2JoltCount++;
                        break;
                    case 3:
                        Difference3JoltCount++;
                        break;
                }

                previousItem = currentItem;
            }
            Difference3JoltCount++; //for last adapter

            return new Tuple<int, int, int>(Difference1JoltCount, Difference2JoltCount, Difference3JoltCount);
        }
    }
}
