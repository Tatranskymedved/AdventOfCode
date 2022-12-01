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
            //var path = @".\Days\Day06\input2.txt";
            var path = @".\Days\Day06\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-', ',' };

            var manager = new LanternfishManager(str, charsToRemove);

            return manager.SimulateDays(256);
        }
    }

    class LanternfishManager
    {
        public double[] arr = new double[9];
        List<Lanternfish> fishes = new List<Lanternfish>();

        public LanternfishManager(string[] lines, char[] charsToRemove)
        {
            var str = lines.First().Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
            fishes.AddRange(str.Select(a => new Lanternfish(Convert.ToDouble(a))));

            str.Select(a => Convert.ToInt32(a)).ToList().ForEach(a => arr[a] += 1);
        }

        public double SimulateDays(double days)
        {
            ///Part 1
            //var tempList = new List<Lanternfish>();
            //for (double i = 0; i < days; i++)
            //{
            //    tempList.Clear();
            //    foreach (var fish in fishes)
            //    {
            //        var newSpawn = fish.NextDay();
            //        if (newSpawn != null)
            //        {
            //            tempList.Add(newSpawn);
            //        }
            //    }
            //    fishes.AddRange(tempList);
            //}

            //return fishes.Count;

            double tmp = 0;
            for (double i = 0; i < days; i++)
            {
                tmp = arr[0];
                arr[0] = arr[1];
                arr[1] = arr[2];
                arr[2] = arr[3];
                arr[3] = arr[4];
                arr[4] = arr[5];
                arr[5] = arr[6];
                arr[6] = arr[7];
                arr[7] = arr[8];
                arr[8] = tmp;

                arr[6] += tmp;
            }

            return arr[0] + arr[1] + arr[2] + arr[3] + arr[4] + arr[5] + arr[6] + arr[7] + arr[8];
        }
    }

    class Lanternfish
    {
        public double Age { get; set; }
        public Lanternfish() : this(8) { }
        public Lanternfish(double age)
        {
            Age = age;
        }

        public Lanternfish NextDay()
        {
            Age--;

            if (Age < 0)
            {
                Age = 6;
                return new Lanternfish();
            }

            return null;
        }
    }
}
