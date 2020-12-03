using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day3 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day03\input.txt";

            var originalMap = System.IO.File.ReadAllLines(path);
            var map = new Map(originalMap);

            //Part 1
            //while (map.Jump(3, 1)) { }
            //return map.Counter_TreeOccured;

            //Part 2
            while (map.Jump(1, 1)) { }
            int a = map.Counter_TreeOccured;

            map.Reset();
            while (map.Jump(3, 1)) { }
            int b = map.Counter_TreeOccured;

            map.Reset();
            while (map.Jump(5, 1)) { }
            int c = map.Counter_TreeOccured;

            map.Reset();
            while (map.Jump(7, 1)) { }
            int d = map.Counter_TreeOccured;

            map.Reset();
            while (map.Jump(1, 2)) { }
            int e = map.Counter_TreeOccured;

            //Must be converted to Double, number is too big
            return Convert.ToDouble(a) * b * c * d * e;
        }
    }

    internal class Map
    {
        public Vec2D CurrentPosition { get; set; }
        public string[] Lines { get; set; }
        public int Counter_TreeOccured { get; set; }
        //public Dictionary<string, int> SlopeCounter_Tree { get; set; } = new Dictionary<string, int>();

        public const char cTree = '#';
        public const char cFreeSpace = '.';

        public Map(string[] Lines)
        {
            this.Lines = Lines;
            Reset();
        }

        public void Reset()
        {
            Counter_TreeOccured = 0;
            CurrentPosition = new Vec2D(0, 0);
        }

        public bool Jump(int stepX, int stepY, string slope = null)
        {
            var newPos = CurrentPosition + new Vec2D(stepX, stepY);

            //Is on map
            if (Lines.Length > newPos.Y)
            {
                newPos.UpdateXIfOutOfBoundary(Lines[newPos.Y].Length);

                if (Lines[newPos.Y][newPos.X] == cTree)
                {
                    Counter_TreeOccured++;
                }
            }
            else //Out of the map
            {
                return false;
            }

            this.CurrentPosition = newPos;
            return true;
        }
    }

    struct Vec2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vec2D(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void UpdateXIfOutOfBoundary(int lineWidth)
        {
            X = X - ((X / lineWidth) * lineWidth);
        }

        public static Vec2D operator +(Vec2D a, Vec2D b)
        {
            return new Vec2D(a.X + b.X, a.Y + b.Y);
        }
    }
}
