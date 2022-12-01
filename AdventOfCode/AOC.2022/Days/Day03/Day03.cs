using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day03 : ADay
    {
        int bitSize;

        public override double Main_Double()
        {
            var path = @".\Days\Day03\input.txt";
            //var path = @".\Days\Day03\myInput.txt";

            var str = System.IO.File.ReadAllLines(path).ToList();
            bitSize = str.First().Length;

            //Part 1
            //var array = new int[bitSize];
            //for (int i = 0; i < str.Length; i++)
            //{
            //    var line = str[i];
            //    for (int j = 0; j < bitSize; j++)
            //    {
            //        if (line[j] == '1')
            //            array[j]++;
            //    }
            //}

            //int mostCommon = 0;
            //for (int i = 0; i < array.Length; i++)
            //{
            //    if (array[i] > (str.Length / 2))
            //    {
            //        mostCommon |= 1 << (array.Length - i - 1);
            //    }
            //}
            //int leastCommon = Convert.ToInt32(Math.Pow(2, array.Length)) - mostCommon - 1;

            //return mostCommon * leastCommon;

            //Part 2
            var oxygen = convertFromStringToInt(removeFromList(str.ToList(), true));
            var CO2 = convertFromStringToInt(removeFromList(str, false));
            return oxygen * CO2;
        }

        public int convertFromStringToInt(string number)
        {
            int result = 0;
            for (int i = 0; i < number.Length; i++)
            {
                result |= (number[i] == '1' ? 1 : 0)  << (number.Length - i - 1);
            }
            return result;
        }


        public string removeFromList(List<string> list, bool isMostCommonValue)
        {
            for (int j = 0; j < bitSize; j++)
            {
                double count = list.Count() / 2d;
                int one = list.Count(a => a[j] == '1');
                if (isMostCommonValue)
                {
                    if (one < count)
                    {
                        list.Where(a => a[j] == '1').ToList().ForEach(a => list.Remove(a));
                    }
                    else if(one == count)
                    {
                        list.Where(a => a[j] == '0').ToList().ForEach(a => list.Remove(a));
                    }
                    else
                    {
                        list.Where(a => a[j] == '0').ToList().ForEach(a => list.Remove(a));
                    }
                }

                if (isMostCommonValue == false)
                {
                    if (one > count)
                    {
                        list.Where(a => a[j] == '1').ToList().ForEach(a => list.Remove(a));
                    }
                    else if (one == count)
                    {
                        list.Where(a => a[j] == '1').ToList().ForEach(a => list.Remove(a));
                    }
                    else
                    {
                        list.Where(a => a[j] == '0').ToList().ForEach(a => list.Remove(a));
                    }
                }



                if (list.Count() == 1)
                    return list.First();
            }

            return "none";
        }
    }
}
