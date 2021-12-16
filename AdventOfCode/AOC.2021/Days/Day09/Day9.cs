using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day9 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day09\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var board = new HeightBoard(str);

            //Part 1
            //return board.riskFactor;
            return board.Get3LargestBasins();
        }
    }

    class HeightBoard
    {
        HeightPoint[,] array = new HeightPoint[0, 0];
        private int width;
        private int height;
        public int riskFactor;

        public HeightBoard(string[] str)
        {
            width = str.First().Length;
            height = str.Length;
            array = new HeightPoint[height, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    array[y, x] = new HeightPoint(Convert.ToInt32(str[y][x].ToString()));
                }
            }


            int sum = 0;
            int cnt = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var p = array[y, x].Value; //current heightpoint's value

                    if (y > 0)
                        if (p >= array[y - 1, x].Value) array[y, x].IsLowPoint = false;

                    if (y < height - 1)
                        if (p >= array[y + 1, x].Value) array[y, x].IsLowPoint = false;

                    if (x > 0)
                        if (p >= array[y, x - 1].Value) array[y, x].IsLowPoint = false;
                    if (x < width - 1)
                        if (p >= array[y, x + 1].Value) array[y, x].IsLowPoint = false;

                    if (array[y, x].IsLowPoint.HasValue == false)
                    {
                        sum += array[y, x].Value;
                        cnt++;
                        array[y, x].IsLowPoint = true;
                    }
                }
            }
            riskFactor = sum + cnt;
        }

        public int Get3LargestBasins()
        {
            int basinIndex = 1;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var res = CrawlSurrondingsToAssingBasin(y, x, basinIndex);
                    if (res)
                        basinIndex++;
                }
            }

            var arr = new int[basinIndex + 1];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var b = array[y, x].Basin;
                    if (b != 0)
                        arr[b]++;
                }
            }

            var firstMax = arr.Max();
            var tmp = arr.ToList();
            tmp.Remove(firstMax);
            var secondMax = tmp.Max();
            tmp.Remove(secondMax);
            var thirdMax = tmp.Max();

            return firstMax * secondMax * thirdMax;
        }

        private bool CrawlSurrondingsToAssingBasin(int y, int x, int basinIndex)
        {
            if (array[y, x].Basin != 0) return false;
            if (array[y, x].Value == 9) return false;

            array[y, x].Basin = basinIndex;

            if (y > 0)
                CrawlSurrondingsToAssingBasin(y - 1, x, basinIndex);
            if (y < height - 1)
                CrawlSurrondingsToAssingBasin(y + 1, x, basinIndex);
            if (x > 0)
                CrawlSurrondingsToAssingBasin(y, x - 1, basinIndex);
            if (x < width - 1)
                CrawlSurrondingsToAssingBasin(y, x + 1, basinIndex);

            return true;
        }
    }

    class HeightPoint
    {
        public int Value { get; set; }
        public bool? IsLowPoint { get; set; }
        public int Basin { get; set; }
        public HeightPoint(int val)
        {
            Value = val;
        }
    }
}
