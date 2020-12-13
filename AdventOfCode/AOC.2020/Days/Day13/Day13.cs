using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day13 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day13\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var bd = new BusDepo(str[0], str[1]);
            //return bd.GetEarliestDepartureBusNumTimesMinutesToWait();
            return bd.GetSolutionPart2();
        }
    }

    class BusDepo
    {
        public int DepartureTime { get; set; }
        public Dictionary<long, int> BusDict { get; set; } = new Dictionary<long, int>();
        public KeyValuePair<long, int> EarliestBus = new KeyValuePair<long, int>(0, int.MaxValue);
        public BusDepo(string departureTime, string busLine)
        {
            DepartureTime = Convert.ToInt32(departureTime);
            var allEntries = busLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < allEntries.Count(); i++)
            {
                if (allEntries[i][0] != 'x')
                {
                    var bus = Convert.ToInt64(allEntries[i]);

                    var mod = DepartureTime % bus;
                    var stepsToBus = bus - mod;
                    if (stepsToBus < EarliestBus.Value)
                        EarliestBus = new KeyValuePair<long, int>(bus, i);

                    BusDict.Add(bus, i);
                }
            }
        }
        //Part 1
        public long GetEarliestDepartureBusNumTimesMinutesToWait()
        {
            return EarliestBus.Value * EarliestBus.Key;
        }

        //At the end used solution of:
        // - https://www.reddit.com/r/adventofcode/comments/kc4njx/2020_day_13_solutions/
        // - https://github.com/RaczeQ/AdventOfCode2020/tree/master/Day13
        //
        // It iterates through all the elements from top to bottom increasing increment by value. Once value % t.Key = 0, then we found the period for that number and adds it.
        // After that we are looking for least common multiplier (LCM) to change "incrementing step".
        public long GetSolutionPart2()
        {
            var x = BusDict.OrderByDescending(a => a, new KVPComparer()).ToList();
            var timestamp = x.First().Key - x.First().Value;
            var period = x.First().Key;
            for (var busIndex = 1; busIndex <= x.Count; busIndex++)
            {
                while (x.Take(busIndex).Any(t => (timestamp + t.Value) % t.Key != 0))
                {
                    timestamp += period;
                }

                period = x.Take(busIndex).Select(t => t.Key).Aggregate(LCM);
            }

            return timestamp;


            // Chinese remainder theorem
            // We can use it because all the numbers are primes
            // https://cs.wikipedia.org/wiki/%C4%8C%C3%ADnsk%C3%A1_v%C4%9Bta_o_zbytc%C3%ADch

            //tbd

            ////Iterative solution -takes too long
            //long n = 0;
            //for (; ; n++)
            //{                
            //    bool isFaulty = false;
            //    foreach (var busPair in BusDict)
            //    {
            //        var mod = n % busPair.Key;
            //        if ((busPair.Value == 0 && (mod) != 0)
            //            || (busPair.Value != 0 && mod + busPair.Value != busPair.Key))
            //        {
            //            isFaulty = true;
            //            break;
            //        }
            //    }
            //    if (!isFaulty)
            //    {
            //        foreach(var bus in BusDict) Console.WriteLine(bus + " " + n % bus.Key);
            //        return n;
            //    }
            //}
            //return 0;
        }

        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }
    }

    class KVPComparer : IComparer<KeyValuePair<long, int>>
    {
        public int Compare([AllowNull] KeyValuePair<long, int> x, [AllowNull] KeyValuePair<long, int> y)
        {
            return x.Key.CompareTo(y.Key);
        }
    }
}
