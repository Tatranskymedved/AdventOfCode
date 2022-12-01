using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day11 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day11\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };


            int maxSteps = 10000;
            var b = new DumboOctopusBoard(str);
            int step = b.MakeSteps(maxSteps);

            //return b.FlashCounter;
            return step;
        }
    }

    class DumboOctopusBoard
    {
        int width = 0;
        int height = 0;
        DumboOctopus[,] array;

        public int FlashCounter { get; set; } = 0;

        public DumboOctopusBoard(string[] str)
        {
            width = str.First().Length;
            height = str.Length;
            array = new DumboOctopus[height, width];

            for (int y = 0; y < str.Length; y++)
            {
                var row = str[y];
                for (int x = 0; x < row.Length; x++)
                {
                    array[y, x] = new DumboOctopus(Convert.ToInt32(row[x].ToString()));
                }
            }

        }

        public int MakeSteps(int maxSteps)
        {
            for (int i = 0; i < maxSteps; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        MakeStepForOctopuses(y, x);
                    }
                }

                bool anyNotFlashed = false;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (array[y, x].HasFlashed == false)
                        {
                            anyNotFlashed = true;
                            break;
                        }
                    }
                    if (anyNotFlashed)
                    {
                        break;
                    }
                }
                if (anyNotFlashed == false) //All flashinig together
                    return i + 1;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        array[y, x].Reset();
                    }
                }
            }

            return 0;
        }


        private bool MakeStepForOctopuses(int y, int x)
        {
            var hasFlashed = array[y, x].MakeStepAndReturnIfFlashed();
            if (hasFlashed == false) return false;

            FlashCounter++;

            if (y > 0)
            {
                MakeStepForOctopuses(y - 1, x);
                if (x > 0)
                {
                    MakeStepForOctopuses(y - 1, x - 1);
                }
                if (x < width - 1)
                {
                    MakeStepForOctopuses(y - 1, x + 1);
                }
            }
            if (y < height - 1)
            {
                MakeStepForOctopuses(y + 1, x);
                if (x > 0)
                {
                    MakeStepForOctopuses(y + 1, x - 1);
                }
                if (x < width - 1)
                {
                    MakeStepForOctopuses(y + 1, x + 1);
                }
            }

            if (x > 0)
            {
                MakeStepForOctopuses(y, x - 1);
            }
            if (x < width - 1)
            {
                MakeStepForOctopuses(y, x + 1);
            }

            return true;
        }
    }

    class DumboOctopus
    {
        public bool HasFlashed { get; set; } = false;
        public int Value { get; set; }

        public DumboOctopus(int value)
        {
            Value = value;
        }

        public bool MakeStepAndReturnIfFlashed()
        {
            Value++;
            if (Value > 9 && (HasFlashed == false))
            {
                HasFlashed = true;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            HasFlashed = false;
            if (Value > 9)
                Value = 0;
        }
    }
}